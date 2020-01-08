using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories
{
    public interface ISolicitacaoCorridaRepository : IBaseRepository<SolicitacaoCorrida>
    {
        Task<int> ObterNumeroAceitacoes(SolicitacaoCorrida solicitacao);
        Task<bool> RegistrarAcaoTaxista(SolicitacaoCorrida solicitacao, Taxista taxista, AcaoTaxistaSolicitacaoCorrida acao);
        Task<IEnumerable<Taxista>> ClassificarTaxistas(SolicitacaoCorrida solicitacao);
        Task<bool> AlterarStatusMonitoramento(SolicitacaoCorrida solicitacao, StatusMonitoramentoSolicitacaoCorrida status);
        Task<bool> AlterarSituacao(SolicitacaoCorrida solicitacao, SituacaoSolicitacaoCorrida situacao);
        AcaoTaxistaSolicitacaoCorrida buscarAcaoTaxista(string idTaxista, Guid idSolicitacaoCorrida);
    }
}
