using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Compat;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Proxies
{
    public class ProxyEmergencia : IProxyEmergencia
    {
        IHubContext<HubNotificacoes> hubNotificacoes;
        IHubContext<HubLocalizacaoTaxista> hubLocalizacaoTaxista; // COMPAT

        public ProxyEmergencia(
            IHubContext<HubNotificacoes> hubNotificacoes,
            IHubContext<HubLocalizacaoTaxista> hubLocalizacaoTaxista) // COMPAT
        {
            this.hubNotificacoes = hubNotificacoes;
            this.hubLocalizacaoTaxista = hubLocalizacaoTaxista; // COMPAT
        }

        public async Task EnviarPanico(EmergenciaSummary emergencia)
        {
            var connections = HubNotificacoes.connections.GetConnections(emergencia.IdTaxista).ToList().AsReadOnly();
            await hubNotificacoes.Clients.AllExcept(connections).SendAsync("panico", emergencia);
            await hubLocalizacaoTaxista.Clients.AllExcept(connections).SendAsync("panico", emergencia); // COMPAT
        }
    }
}
