using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies
{
    public interface IProxySolicitacaoCorrida
    {
        Task AtivarTaxista(Taxista taxista, SolicitacaoCorridaSummary solicitacaoCorrida);
        Task AtivarTaxistas(IEnumerable<Taxista> taxistas, SolicitacaoCorridaSummary solicitacaoCorrida);
        Task InformarAcaoTaxista(Taxista taxista, SolicitacaoCorrida solicitacao, AcaoTaxistaSolicitacaoCorrida acao);
    }
}
