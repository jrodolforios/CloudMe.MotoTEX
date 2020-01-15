using CloudMe.MotoTEX.Domain.Notifications.Abstracts;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Background
{
    public class PoolLocalizacaoPassageiro : BackgroundService, IPoolLocalizacaoPassageiro
    {
        public int Timeout { get; set; } = 5000;

        private IHubContext<HubLocalizacaoPassageiro> _hubContext;

        public PoolLocalizacaoPassageiro(IHubContext<HubLocalizacaoPassageiro> hubContext) : base()
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubContext.Clients.All.SendAsync("EnviarLocalizacao");
                await Task.Delay(Timeout, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
