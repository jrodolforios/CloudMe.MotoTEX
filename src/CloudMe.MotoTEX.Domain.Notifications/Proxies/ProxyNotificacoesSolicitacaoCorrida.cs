using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;

namespace CloudMe.MotoTEX.Domain.Notifications.Proxies
{
    public class ProxyNotificacoesSolicitacaoCorrida : IProxyNotificacoesSolicitacaoCorrida
    {
        IHubContext<HubNotificaoes> hubContext;

        public ProxyNotificacoesSolicitacaoCorrida(IHubContext<HubNotificaoes> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task AtivarTaxista(Taxista taxista, SolicitacaoCorridaSummary solicitacaoCorrida)
        {
            await hubContext.Clients.User(taxista.IdUsuario.ToString()).SendAsync("sol_corr_ativar_tx", solicitacaoCorrida);
        }

        public async Task AtivarTaxistas(IEnumerable<Taxista> taxistas, SolicitacaoCorridaSummary solicitacaoCorrida)
        {
            await hubContext.Clients.Users(taxistas.Select(x => x.IdUsuario.ToString()).ToList()).SendAsync("sol_corr_ativar_tx", solicitacaoCorrida);
        }
    }
}
