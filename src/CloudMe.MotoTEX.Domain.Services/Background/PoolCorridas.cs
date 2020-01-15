using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Enums;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using Microsoft.Extensions.Configuration;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;

namespace CloudMe.MotoTEX.Domain.Services.Background
{
    public class PoolCorridas : BackgroundService
    {

        IServiceScopeFactory scopeFactory = null;
        public IConfiguration configuration { get; }

        public int TimeoutEmCurso { get; set; } = 1440;
        public int TimeoutEmEspera { get; set; } = 1440;
        public int TimeoutSolicitada { get; set; } = 1440;
        public int TimeoutAtrasada { get; set; } = 15;
        public int DelayVarreduraAtraso { get; set; } = 5000;

        public PoolCorridas(IServiceScopeFactory _scopeFactory, IConfiguration _configuration) : base()
        {
            scopeFactory = _scopeFactory;
            configuration = _configuration;
        }

        private void Inicializar()
        {
            TimeoutEmCurso = configuration.GetValue<int>("MonitorCorridas:TimeoutEmCurso");
            TimeoutEmEspera = configuration.GetValue<int>("MonitorCorridas:TimeoutEmEspera");
            TimeoutSolicitada = configuration.GetValue<int>("MonitorCorridas:TimeoutSolicitada");
            TimeoutAtrasada = configuration.GetValue<int>("MonitorCorridas:TimeoutAtrasada");
            DelayVarreduraAtraso = configuration.GetValue<int>("MonitorCorridas:DelayVarreduraAtraso");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Inicializar();

            // obtém as solicitações de corrida
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var corridasRepo = scope.ServiceProvider.GetRequiredService<ICorridaRepository>();

                    var obsoletasEmCurso = corridasRepo.Search(x =>
                        x.Status == StatusCorrida.EmCurso &&
                        (DateTime.Now - x.Updated).Minutes > TimeoutEmCurso);

                    var obsoletasEmEspera = corridasRepo.Search(x =>
                        x.Status == StatusCorrida.EmEspera &&
                        (DateTime.Now - x.Updated).Minutes > TimeoutEmEspera);

                    var obsoletasSolicitadas = corridasRepo.Search(x =>
                        x.Status == StatusCorrida.Solicitada &&
                        (DateTime.Now - x.Updated).Minutes > TimeoutSolicitada);

                    var atrasadas = corridasRepo.Search(x =>
                        x.Status == StatusCorrida.Agendada &&
                        (DateTime.Now - x.Solicitacao.Data.Value).Minutes > TimeoutAtrasada, new[] { "Solicitacao" });

                    var corridasEncerrar = obsoletasEmCurso
                        .Union(obsoletasEmEspera)
                        .Union(obsoletasSolicitadas)
                        .Union(atrasadas);

                    if (corridasEncerrar.Count() > 0)
                    {
                        foreach (var corrida in corridasEncerrar)
                        {
                            corrida.Status = StatusCorrida.Cancelada;
                            await corridasRepo.ModifyAsync(corrida);
                        }

                        await unitOfWork.CommitAsync();
                    }
                }

                await Task.Delay(DelayVarreduraAtraso, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
