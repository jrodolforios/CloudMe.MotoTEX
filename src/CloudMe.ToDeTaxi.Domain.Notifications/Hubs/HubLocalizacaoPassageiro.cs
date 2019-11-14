﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Notifications.Hubs
{
    public class HubLocalizacaoPassageiro : Hub
    {
        public async Task SolicitarLocalizacao()
        {
            await Clients.All.SendAsync("EnviarLocalizacao");
        }
    }
}
