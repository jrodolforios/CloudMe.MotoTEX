using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubLocalizacaoTaxista : Hub
    {
        public async Task SolicitarLocalizacao()
        {
            await Clients.All.SendAsync("EnviarLocalizacao");
        }
    }
}
