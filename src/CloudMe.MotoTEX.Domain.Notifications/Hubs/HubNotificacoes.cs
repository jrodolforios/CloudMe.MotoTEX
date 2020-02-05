using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubNotificaoes : UserMappedHub<HubNotificaoes>
    {
        public HubNotificaoes(IUsuarioRepository usuarioRepository, IHubContext<UserMappedHub<HubNotificaoes>> hubContext)
            : base(usuarioRepository, hubContext)
        {
        }
    }
}
