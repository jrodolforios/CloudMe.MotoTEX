using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class HubNotificaoesAdmin : UserMappedHub<HubNotificaoesAdmin>
    {
        public HubNotificaoesAdmin(IUsuarioRepository usuarioRepository, IHubContext<UserMappedHub<HubNotificaoesAdmin>> hubContext)
            : base(usuarioRepository, hubContext)
        {
        }
    }
}
