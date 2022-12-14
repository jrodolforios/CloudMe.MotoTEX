using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Enums;
using System.Linq;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Model;
using System.Linq.Expressions;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class SolicitacaoCorridaService : ServiceBase<SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>, ISolicitacaoCorridaService
    {
        private string[] defaultPaths = { "LocalizacaoOrigem", "LocalizacaoDestino" };

        private readonly ISolicitacaoCorridaRepository _SolicitacaoCorridaRepository;
        private readonly ITaxistaRepository _TaxistaRepository;
        private readonly IProxySolicitacaoCorrida _ProxyNotificacoesSolicitacaoCorrida;

        public SolicitacaoCorridaService(
            ISolicitacaoCorridaRepository SolicitacaoCorridaRepository,
            ITaxistaRepository taxistaRepository,
            IProxySolicitacaoCorrida proxyNotificacoesSolicitacaoCorrida)
        {
            _SolicitacaoCorridaRepository = SolicitacaoCorridaRepository;
            _TaxistaRepository = taxistaRepository;
            _ProxyNotificacoesSolicitacaoCorrida = proxyNotificacoesSolicitacaoCorrida;
        }

        public override string GetTag()
        {
            return "solicitacao_corrida";
        }

        protected override async Task<SolicitacaoCorrida> CreateEntryAsync(SolicitacaoCorridaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new SolicitacaoCorrida
                {
                    Id = summary.Id,
                    IdPassageiro = summary.IdPassageiro,
                    IdLocalizacaoOrigem = summary.IdLocalizacaoOrigem,
                    IdLocalizacaoDestino = summary.IdLocalizacaoDestino,
                    IdRota = summary.IdRota,
                    IdFaixaDesconto = summary.IdFaixaDesconto,
                    IdFormaPagamento = summary.IdFormaPagamento,
                    TipoAtendimento = summary.TipoAtendimento,
                    Data = summary.Data,
                    ETA = summary.ETA,
                    TempoDisponivel = summary.TempoDisponivel,
                    ValorEstimado = summary.ValorEstimado,
                    ValorProposto = summary.ValorProposto,
                    Situacao = summary.Situacao,
                    IsInterUrbano = summary.IsInterUrbano
                };
            });
        }

        protected override async Task<SolicitacaoCorridaSummary> CreateSummaryAsync(SolicitacaoCorrida entry)
        {
            return await Task.Run(async () =>
            {
                if (entry == null) return default;

                return new SolicitacaoCorridaSummary
                {
                    Id = entry.Id,
                    IdPassageiro = entry.IdPassageiro,
                    IdLocalizacaoOrigem = entry.IdLocalizacaoOrigem,
                    IdLocalizacaoDestino = entry.IdLocalizacaoDestino,
                    IdRota = entry.IdRota,
                    IdFaixaDesconto = entry.IdFaixaDesconto,
                    IdFormaPagamento = entry.IdFormaPagamento,
                    TipoAtendimento = entry.TipoAtendimento,
                    Data = entry.Data,
                    ETA = entry.ETA,
                    TempoDisponivel = entry.TempoDisponivel,
                    ValorEstimado = entry.ValorEstimado,
                    ValorProposto = entry.ValorProposto,
                    Situacao = entry.Situacao,
                    IsInterUrbano = entry.IsInterUrbano,
                    latitudeOrigem = entry.LocalizacaoOrigem != null ? entry.LocalizacaoOrigem.Latitude : "0",
                    longitudeOrigem = entry.LocalizacaoOrigem != null ? entry.LocalizacaoOrigem.Longitude : "0",
                    latitudeDestino = entry.LocalizacaoDestino != null ? entry.LocalizacaoDestino.Latitude : "0",
                    longitudeDestino = entry.LocalizacaoDestino != null ? entry.LocalizacaoDestino.Longitude : "0",
                    IdxFaixaBusca = entry.IdxFaixaBusca,
                    StatusMonitoramento = entry.StatusMonitoramento
                };
            });
        }

        protected override Guid GetKeyFromSummary(SolicitacaoCorridaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<SolicitacaoCorrida> GetRepository()
        {
            return _SolicitacaoCorridaRepository;
        }

        protected override void UpdateEntry(SolicitacaoCorrida entry, SolicitacaoCorridaSummary summary)
        {
            entry.IdPassageiro = summary.IdPassageiro;
            entry.IdLocalizacaoOrigem = summary.IdLocalizacaoOrigem;
            entry.IdLocalizacaoDestino = summary.IdLocalizacaoDestino;
            entry.IdRota = summary.IdRota;
            entry.IdFaixaDesconto = summary.IdFaixaDesconto;
            entry.IdFormaPagamento = summary.IdFormaPagamento;
            entry.TipoAtendimento = summary.TipoAtendimento;
            entry.Data = summary.Data;
            entry.ETA = summary.ETA;
            entry.TempoDisponivel = summary.TempoDisponivel;
            entry.ValorEstimado = summary.ValorEstimado;
            entry.ValorProposto = summary.ValorProposto;
            entry.Situacao = summary.Situacao;
            entry.IsInterUrbano = summary.IsInterUrbano;
        }

        protected override void ValidateSummary(SolicitacaoCorridaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "SolicitacaoCorrida: summário é obrigatório"));
            }

            if (summary.IdPassageiro.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdPassageiro", "SolicitacaoCorrida: passasgeiro inexistente ou não informado"));
            }

            if (summary.IdLocalizacaoOrigem.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdLocalizacaoOrigem", "SolicitacaoCorrida: local de origem inexistente ou não informado"));
            }

            if (summary.IdLocalizacaoDestino.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdLocalizacaoDestino", "SolicitacaoCorrida: local de destino inexistente ou não informado"));
            }

            /*if (summary.IdRota.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdRota", "Passageiro: rota inexistente ou não informado"));
            }*/

            if (summary.IdFormaPagamento.Equals(Guid.Empty))
            {
                this.AddNotification(new Notification("IdFormaPagamento", "SolicitacaoCorrida: forma de pagamento inexistente ou não informado"));
            }

            if (summary.TipoAtendimento == Enums.TipoAtendimento.Indefinido)
            {
                this.AddNotification(new Notification("TipoAtendimento", "SolicitacaoCorrida: tipo de atendimento não definido"));
            }

            /*if (summary.Situacao == Enums.SituacaoSolicitacaoCorrida.Indefinido)
            {
                this.AddNotification(new Notification("Situacao", "Passageiro: situação não definida"));
            }*/
        }

        public override async Task<SolicitacaoCorrida> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<SolicitacaoCorrida>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<SolicitacaoCorrida>> Search(Expression<Func<SolicitacaoCorrida, bool>> where, string[] paths = null, Pagination pagination = null)
        {
            return await base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, pagination);
        }

        public async Task<bool> RegistrarAcaoTaxista(Guid id_solicitacao, Guid id_taxista, AcaoTaxistaSolicitacaoCorrida acao)
        {
            var solicitacao = await _SolicitacaoCorridaRepository.FindByIdAsync(id_solicitacao);
            if (solicitacao is null)
            {
                AddNotification(new Notification("Solicitações de corrida", "Registrar ação taxista: solicitação não encontrada"));
                return false;
            }

            var taxista = await _TaxistaRepository.FindByIdAsync(id_taxista);
            if (taxista is null)
            {
                AddNotification(new Notification("Solicitações de corrida", "Registrar ação taxista: taxista não encontrado"));
                return false;
            }

            await _ProxyNotificacoesSolicitacaoCorrida.InformarAcaoTaxista(taxista, solicitacao, acao);

            return await _SolicitacaoCorridaRepository.RegistrarAcaoTaxista(solicitacao, taxista, acao);
        }

        public async Task<IList<SolicitacaoCorridaSummary>> RecuperarSolicitacoesEmEspera(Guid idTaxista)
        {
            var taxista = await _TaxistaRepository.FindByIdAsync(idTaxista);
            if (taxista is null)
            {
                AddNotification(new Notification("Solicitações de corrida", "RecuperarSolicitacoesEmEspera: taxista não encontrado"));
                return default;
            }

            var solicitacoesSumaries = new List<SolicitacaoCorridaSummary>();

            var solicitacoesEntries = await _SolicitacaoCorridaRepository.Search(
                x => 
                x.Situacao == SituacaoSolicitacaoCorrida.Indefinido || 
                x.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao);

            foreach (var item in solicitacoesEntries)
            {
                AcaoTaxistaSolicitacaoCorrida acao = await _SolicitacaoCorridaRepository.buscarAcaoTaxista(idTaxista, item.Id);

                if (acao != AcaoTaxistaSolicitacaoCorrida.Recusada)
                {
                    var solicitacaoSummary = await CreateSummaryAsync(item);
                    solicitacoesSumaries.Add(solicitacaoSummary);
                }
            }

            return solicitacoesSumaries;
        }

        public async Task<IEnumerable<SolicitacaoCorrida>> RecuperarSolicitacoesAtivas()
        {
            return await _SolicitacaoCorridaRepository.Search(x => x.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao);
        }

        public async Task<EstatisticasSolicitacoesCorrida> ObterEstatisticas(DateTime? inicio, DateTime? fim)
        {
            inicio = inicio ?? DateTime.MinValue;
            fim = fim ?? DateTime.MaxValue;

            var estatisticas = new EstatisticasSolicitacoesCorrida();

            var solicitacoes = await _SolicitacaoCorridaRepository.Search(x => x.Inserted >= inicio && x.Inserted <= fim);

            if (solicitacoes.Count() > 0)
            {
                estatisticas.Total = solicitacoes.Count();
                estatisticas.Agendadas = solicitacoes.Where(x => x.TipoAtendimento == TipoAtendimento.Agendado).Count();
                estatisticas.Interurbanas = solicitacoes.Where(x => x.IsInterUrbano).Count();
                estatisticas.ValorMedio = solicitacoes.Sum(x => x.ValorEstimado ?? 0) / estatisticas.Total;
                estatisticas.EmAndamento = solicitacoes.Where(x => x.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao).Count();
                estatisticas.Atendidas = solicitacoes.Where(x => x.Situacao == SituacaoSolicitacaoCorrida.Aceita).Count();
                estatisticas.NaoAtendidas = solicitacoes.Where(x => x.Situacao == SituacaoSolicitacaoCorrida.NaoAtendida).Count();
            }

            return estatisticas;
        }
    }
}
