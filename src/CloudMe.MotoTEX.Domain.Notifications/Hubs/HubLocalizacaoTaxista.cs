using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubLocalizacaoTaxista : UserMappedHub<HubLocalizacaoTaxista>
    {
        public HubLocalizacaoTaxista(IUsuarioRepository _usuarioRepository, IHubContext<UserMappedHub<HubLocalizacaoTaxista>> hubContext)
            : base(_usuarioRepository, hubContext)
        {
        }

        public async Task SolicitarLocalizacao()
        {
            await hubContext.Clients.All.SendAsync("EnviarLocalizacao");
        }
    }
}
