using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Services
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
            List<CorridaSummary> corridaSummaries = new List<CorridaSummary>();

            var solicitacoes = _SolicitacaoCorridaRepository.FindAll().Where(x => x.IdPassageiro == id);
            var corridas = _CorridaRepository.FindAll().Where(x => solicitacoes.Any(y => y.Id == x.IdSolicitacao));

            corridas.ToList().ForEach(async x =>
            {
                var summary = await CreateSummaryAsync(x);
                corridaSummaries.Add(summary);
            });

            return corridaSummaries;
        }

        public async Task<IEnumerable<CorridaSummary>> GetAllSummariesByTaxistAsync(Guid id)
        {
            List<CorridaSummary> corridaSummaries = new List<CorridaSummary>();

            var corridas = _CorridaRepository.FindAll().Where(x => x.IdTaxista == id);

            corridas.ToList().ForEach(async x =>
            {
                var summary = await CreateSummaryAsync(x);
                corridaSummaries.Add(summary);
            });

            return corridaSummaries;
        }

        protected override Task<Corrida> CreateEntryAsync(CorridaSummary summary)
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
            return Task.FromResult(Corrida);
        }

        protected override Task<CorridaSummary> CreateSummaryAsync(Corrida entry)
        {
            var Corrida = new CorridaSummary
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

            return Task.FromResult(Corrida);
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
            CorridaSummary corrida = null;
            if (_CorridaRepository.FindAll().Any(x => x.IdSolicitacao == id))
                corrida = await CreateSummaryAsync(_CorridaRepository.FindAll().FirstOrDefault(x => x.IdSolicitacao == id));

            return corrida;
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
            var result = _SolicitacaoCorridaRepository.FindAll().Where(x => x.Data >= data).ToList();

            var resultFinal = _CorridaRepository.FindAll().Where(x => result.Any(y => y.Id == x.IdSolicitacao)).ToList();
            var resultEnviar = new List<CorridaSummary>();

            foreach (var x in resultFinal)
            {
                resultEnviar.Add(await CreateSummaryAsync(x));
            }

            return resultEnviar;
        }
    }
}
