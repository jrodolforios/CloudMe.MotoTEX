using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubNotificacoes : UserMappedHub<HubNotificacoes>
    {
        public HubNotificacoes(IUsuarioRepository usuarioRepository, IHubContext<UserMappedHub<HubNotificacoes>> hubContext)
            : base(usuarioRepository, hubContext)
        {
        }
    }
}
