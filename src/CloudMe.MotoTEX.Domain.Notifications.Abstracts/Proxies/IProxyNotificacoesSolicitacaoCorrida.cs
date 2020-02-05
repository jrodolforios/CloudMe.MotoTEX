using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies
{
    public interface IProxyNotificacoesSolicitacaoCorrida
    {
        Task AtivarTaxista(Taxista taxista, SolicitacaoCorridaSummary solicitacaoCorrida);
        Task AtivarTaxistas(IEnumerable<Taxista> taxistas, SolicitacaoCorridaSummary solicitacaoCorrida);
    }
}
