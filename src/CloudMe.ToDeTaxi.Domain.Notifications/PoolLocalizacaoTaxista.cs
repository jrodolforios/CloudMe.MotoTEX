using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstracts;
using CloudMe.ToDeTaxi.Domain.Notifications.Hubs;
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

        public PoolLocalizacaoTaxista(IHubContext<HubLocalizacaoTaxista> hubContext) : base()
        {
            _hubContext = hubContext;
        }

        public async Task SolicitarLocalizacao()
        {
            await _hubContext.Clients.All.SendAsync("EnviarLocalizacao");
        }

        public async Task EnviarPanico(EmergenciaSummary emergencia)
        {
            await _hubContext.Clients.AllExcept(emergencia.IdTaxista.ToString()).SendAsync("panico", emergencia);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SolicitarLocalizacao();
                await Task.Delay(Timeout, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
