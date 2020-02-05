﻿using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubMensagens : UserMappedHub<HubMensagens>
    {
        public HubMensagens(IUsuarioRepository usuarioRepository, IHubContext<UserMappedHub<HubMensagens>> hubContext)
            : base(usuarioRepository, hubContext)
        {
        }
    }
}
