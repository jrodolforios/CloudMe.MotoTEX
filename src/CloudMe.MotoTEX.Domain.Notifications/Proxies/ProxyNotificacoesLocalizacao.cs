using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Proxies
{
    public class ProxyNotificacoesLocalizacao : IProxyNotificacoesLocalizacao
    {
        IHubContext<HubNotificaoesAdmin> hubContextAdmin;

        public ProxyNotificacoesLocalizacao(IHubContext<HubNotificaoesAdmin> hubContextAdmin)
        {
            this.hubContextAdmin = hubContextAdmin;
        }

        public async Task InformarLocalizacaoTaxista(Guid idTaxista, double latitude, double longitude, double angulo)
        {
            await hubContextAdmin.Clients.All.SendAsync("loc_tx", new
            {
                id = idTaxista,
                lat = latitude,
                lgt = longitude,
                angulo
            });
        }

        public async Task InformarLocalizacaoPassageiro(Guid idPassageiro, double latitude, double longitude, double angulo)
        {
            await hubContextAdmin.Clients.All.SendAsync("loc_pass", new
            {
                id = idPassageiro,
                lat = latitude,
                lgt = longitude,
                angulo
            });
        }
    }
}
