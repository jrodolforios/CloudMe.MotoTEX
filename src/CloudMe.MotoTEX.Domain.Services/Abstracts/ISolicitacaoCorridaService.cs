using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface ISolicitacaoCorridaService : IServiceBase<SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>
    {
        Task<bool> RegistrarAcaoTaxista(Guid id_solicitacao, Guid id_taxista, AcaoTaxistaSolicitacaoCorrida acao);
        Task<IList<SolicitacaoCorridaSummary>> RecuperarSolicitacoesEmEspera(Guid idTaxista);
        Task<IEnumerable<SolicitacaoCorrida>> RecuperarSolicitacoesAtivas();
        Task<EstatisticasSolicitacoesCorrida> ObterEstatisticas(DateTime? inicio, DateTime? fim);
    }
}
