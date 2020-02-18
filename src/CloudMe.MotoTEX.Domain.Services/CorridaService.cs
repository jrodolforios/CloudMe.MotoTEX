using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class CorridaService : ServiceBase<Corrida, CorridaSummary, Guid>, ICorridaService
    {
        private readonly ICorridaRepository _CorridaRepository;
        private readonly ISolicitacaoCorridaRepository _SolicitacaoCorridaRepository;

        public CorridaService(ICorridaRepository CorridaRepository, ISolicitacaoCorridaRepository SolicitacaoCorridaRepository)
        {
            _CorridaRepository = CorridaRepository;
            _SolicitacaoCorridaRepository = SolicitacaoCorridaRepository;
        }

        public override string GetTag()
        {
            return "corrida";
        }

        public async Task<IEnumerable<CorridaSummary>> GetAllSummariesByPassangerAsync(Guid id)
        {
            var corridas = await _CorridaRepository.Search(x => x.IdSolicitacao != null && x.Solicitacao.IdPassageiro == id, new[] {"Solicitacao"});
            return await GetAllSummariesAsync(corridas);
        }

        public async Task<IEnumerable<CorridaSummary>> GetAllSummariesByTaxistAsync(Guid id)
        {
            var corridas = await _CorridaRepository.Search(x => x.IdTaxista == id);
            return await GetAllSummariesAsync(corridas);
        }

        protected override async Task<Corrida> CreateEntryAsync(CorridaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                var Corrida = new Corrida
                {
                    Id = summary.Id,
                    IdSolicitacao = summary.IdSolicitacao,
                    IdTaxista = summary.IdTaxista,
                    IdVeiculo = summary.IdVeiculo,
                    IdRotaExecutada = summary.IdRotaExecutada,
                    IdTarifa = summary.IdTarifa,
                    Inicio = summary.Inicio,
                    Fim = summary.Fim,
                    AvaliacaoPassageiro = summary.AvaliacaoPassageiro,
                    AvaliacaoTaxista = summary.AvaliacaoTaxista,
                    Status = summary.Status,
                    TempoEmEspera = summary.TempoEmEspera
                };

                return Corrida;
            });
        }

        protected override async Task<CorridaSummary> CreateSummaryAsync(Corrida entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new CorridaSummary
                {
                    Id = entry.Id,
                    IdSolicitacao = entry.IdSolicitacao,
                    IdTaxista = entry.IdTaxista,
                    IdVeiculo = entry.IdVeiculo,
                    IdRotaExecutada = entry.IdRotaExecutada,
                    IdTarifa = entry.IdTarifa,
                    Inicio = entry.Inicio,
                    Fim = entry.Fim,
                    AvaliacaoPassageiro = entry.AvaliacaoPassageiro,
                    AvaliacaoTaxista = entry.AvaliacaoTaxista,
                    Status = entry.Status,
                    TempoEmEspera = entry.TempoEmEspera
                };
            });
        }

        protected override Guid GetKeyFromSummary(CorridaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Corrida> GetRepository()
        {
            return _CorridaRepository;
        }

        protected override void UpdateEntry(Corrida entry, CorridaSummary summary)
        {
            entry.IdSolicitacao = summary.IdSolicitacao;
            entry.IdTaxista = summary.IdTaxista;
            entry.IdVeiculo = summary.IdVeiculo;
            entry.IdRotaExecutada = summary.IdRotaExecutada;
            entry.IdTarifa = summary.IdTarifa;
            entry.Inicio = summary.Inicio;
            entry.Fim = summary.Fim;
            entry.AvaliacaoPassageiro = summary.AvaliacaoPassageiro;
            entry.AvaliacaoTaxista = summary.AvaliacaoTaxista;
            entry.Status = summary.Status;
            entry.TempoEmEspera = summary.TempoEmEspera;
        }

        protected override void ValidateSummary(CorridaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Corrida: sumário é obrigatório"));
            }

            if (summary.IdSolicitacao.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdSolicitacao", "Corrida: solicitação da corrida inexistente ou não informada"));
            }
        }

        public async Task<CorridaSummary> GetBySolicitacaoCorrida(Guid id)
        {
            return await CreateSummaryAsync((await _CorridaRepository.Search(x => x.IdSolicitacao == id)).FirstOrDefault());
        }

        public async Task<bool> ClassificaTaxista(Guid id, int classificacao)
        {
            var corrida = await _CorridaRepository.FindByIdAsync(id);

            if (corrida != null)
            {
                corrida.AvaliacaoTaxista = (AvaliacaoUsuario)classificacao;

                await _CorridaRepository.ModifyAsync(corrida);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> ClassificaPassageiro(Guid id, int classificacao)
        {
            var corrida = await _CorridaRepository.FindByIdAsync(id);

            if (corrida != null)
            {
                corrida.AvaliacaoPassageiro = (AvaliacaoUsuario)classificacao;

                await _CorridaRepository.ModifyAsync(corrida);
                return true;
            }
            else
                return false;
        }

        public async Task<int> PausarCorrida(Guid id)
        {
            var corrida = await _CorridaRepository.FindByIdAsync(id);
            if (corrida == null)
            {
                AddNotification(new Notification("Pausar corrida", "Corrida não encontrada"));
                return -1;
            }

            if (corrida.Status != StatusCorrida.EmCurso)
            {
                AddNotification(new Notification("Pausar corrida", "Corrida não está em curso"));
                return corrida.TempoEmEspera;
            }

            corrida.UltimaPausa = DateTime.Now;
            corrida.Status = StatusCorrida.EmEspera;
            await _CorridaRepository.ModifyAsync(corrida);

            return corrida.TempoEmEspera;
        }


        public async Task<bool> RetomarCorrida(Guid id)
        {
            var corrida = await _CorridaRepository.FindByIdAsync(id);
            if (corrida == null)
            {
                AddNotification(new Notification("Pausar corrida", "Corrida não encontrada"));
                return false;
            }

            if (corrida.Status != StatusCorrida.EmEspera)
            {
                AddNotification(new Notification("Pausar corrida", "Corrida não está em espera"));
                return false;
            }

            corrida.TempoEmEspera += (DateTime.Now - corrida.UltimaPausa.Value).Seconds;
            corrida.Status = StatusCorrida.EmCurso;
            await _CorridaRepository.ModifyAsync(corrida);

            return true;
        }

        public async Task<List<CorridaSummary>> RecuperarAPartirDeData(DateTime data)
        {
            var corridas = await _CorridaRepository.Search(x => x.Inicio >= data);
            return (await GetAllSummariesAsync(corridas)).ToList();
        }

        public async Task<EstatisticasCorridas> ObterEstatisticas(DateTime? inicio, DateTime? fim)
        {
            inicio = inicio ?? DateTime.MinValue;
            fim = fim ?? DateTime.MaxValue;

            var estatisticas = new EstatisticasCorridas();

            var corridas = await _CorridaRepository.Search(x => x.Inserted >= inicio && x.Inserted <= fim);

            if (corridas.Count() > 0)
            {
                estatisticas.Total = corridas.Count();
                estatisticas.Agendadas = corridas.Where(x => x.Status == StatusCorrida.Agendada).Count();
                estatisticas.Solicitadas = corridas.Where(x => x.Status == StatusCorrida.Solicitada).Count();
                estatisticas.EmCurso = corridas.Where(x => x.Status == StatusCorrida.EmCurso).Count();
                estatisticas.EmEspera = corridas.Where(x => x.Status == StatusCorrida.EmEspera).Count();
                estatisticas.CanceladasTaxista = corridas.Where(x => x.Status == StatusCorrida.Cancelada).Count();
                estatisticas.CanceladasPassageiro = corridas.Where(x => x.Status == StatusCorrida.CanceladaPassageiro).Count();
                estatisticas.Concluidas = corridas.Where(x => x.Status == StatusCorrida.Concluida).Count();
                estatisticas.EmNegociacao = corridas.Where(x => x.Status == StatusCorrida.EmNegociacao).Count();

                var numFinalizadas = estatisticas.CanceladasTaxista + estatisticas.CanceladasPassageiro + estatisticas.Concluidas;

                estatisticas.MediaAvaliacaoTaxista = corridas.Sum(x => (float)x.AvaliacaoTaxista) / numFinalizadas;
                estatisticas.MediaAvaliacaoPassageiro = corridas.Sum(x => (float)x.AvaliacaoPassageiro) / numFinalizadas;
            }

            return estatisticas;
        }
    }
}
