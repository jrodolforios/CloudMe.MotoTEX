using CloudMe.ToDeTaxi.Domain.Notifications.Hubs;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts.Background;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Background
{
    public class PoolLocalizacaoTaxista : BackgroundService, IPoolLocalizacaoTaxista
    {
        public int Timeout { get; set; } = 5000;

        private IHubContext<HubLocalizacaoTaxista> _hubContext;

        PoolLocalizacaoTaxista(IHubContext<HubLocalizacaoTaxista> hubContext) : base()
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
