using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Enums;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using Microsoft.Extensions.Configuration;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using Serilog;

namespace CloudMe.ToDeTaxi.Domain.Services.Background
{
    public class PoolSolicitacoesCorrida : BackgroundService
    {
        private class ParametrosMonitoramento
        {
            public Guid IdSolicitacaoCorrida { get; set; }
            public IServiceScope serviceScope { get; set; }
            public int JanelaAcumulacao { get; set; } = 10000; // default = 10s
            public int JanelaDisponibilidade { get; set; } = 50000; // default = 50s
        }

        private class MonitorSolicitacaoCorrida
        {
            ParametrosMonitoramento parametros { get; set; }
            IServiceScope serviceScope { get; set; }

            public MonitorSolicitacaoCorrida(ParametrosMonitoramento parametros)
            {
                this.parametros = parametros;
                serviceScope = parametros.serviceScope;
            }

            private async Task<int> ObterRespostasTaxistas(SolicitacaoCorrida solicitacao)
            {
                //using (var scope = parametros.serviceScope.CreateScope())
                {
                    var solicitacaoCorridaRepo = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.ObterNumeroAceitacoes(solicitacao);
                }
            }

            private async Task<IEnumerable<Taxista>> ClassificarTaxistasEleitos(SolicitacaoCorrida solicitacao)
            {
                //using (var scope = parametros.serviceScope.CreateScope())
                {
                    var solicitacaoCorridaRepo = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.ClassificarTaxistas(solicitacao);
                }
            }

            private async Task<bool> ElegerTaxista(SolicitacaoCorrida solicitacao, Taxista taxista)
            {
                //using (var scope = parametros.serviceScope.CreateScope())
                {
                    var unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var corridaService = serviceScope.ServiceProvider.GetRequiredService<ICorridaService>();
                    var tarifaService = serviceScope.ServiceProvider.GetRequiredService<ITarifaService>();
                    var veiculoTaxistaService = serviceScope.ServiceProvider.GetRequiredService<IVeiculoTaxistaService>();

                    var tarifaVigente = (await tarifaService.GetAll()).FirstOrDefault();

                    var veiculoTaxista = veiculoTaxistaService.Search(veicTx => veicTx.IdTaxista == taxista.Id && veicTx.Ativo).FirstOrDefault();

                    var corrida = await corridaService.CreateAsync(new CorridaSummary()
                    {
                        IdSolicitacao = solicitacao.Id,
                        IdTaxista = taxista.Id,
                        Status = solicitacao.TipoAtendimento == TipoAtendimento.Agendado ? StatusCorrida.Agendada : StatusCorrida.Solicitada,
                        IdTarifa = tarifaVigente.Id,
                        IdVeiculo = veiculoTaxista.IdVeiculo,
                        Inicio = solicitacao.TipoAtendimento == TipoAtendimento.Agendado ? solicitacao.Data : (DateTime?)null
                    });

                    return await unitOfWork.CommitAsync();
                }
            }

            private async Task<bool> AlterarStatusMonitoramento(SolicitacaoCorrida solicitacao, StatusMonitoramentoSolicitacaoCorrida status)
            {
                //using (var scope = parametros.serviceScope.CreateScope())
                {
                    var solicitacaoCorridaRepo = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, status);
                }
            }

            private async Task<bool> AlterarSituacaoSolicitacao(SolicitacaoCorrida solicitacao, SituacaoSolicitacaoCorrida situacao)
            {
                //using (var scope = parametros.serviceScope.CreateScope())
                {
                    var solicitacaoCorridaRepo = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.AlterarSituacao(solicitacao, situacao);
                }
            }

            public async Task Executar()
            {
                /*
                 * Fluxo:
                 * 
                 * 1 - A solicitação de corrida é gerada (capturada pelo trigger de insert);
                 * 2 - Inicia a janela de acumulação de taxistas interessados na solicitação (10s)
                 * 3 - Findo o perído compreendido no passo 2, é realizada a filtragem e classificação (ordenação) 
                 *     dos taxistas que aceitaram a solicitação, de acordo com os critérios de favoritagem 
                 *     e distância da origem da solicitação
                 * 4 - Se no passo 2 nenhum taxista se candidatar, inicia a janela de disponibilidade (50s), que
                 *     concede a corrida ao primeiro taxista que aceitar solicitação
                 * 5 - Se foi eleito um taxista para a solicitação, esta é marcada como 'aceita' e a corrida é gerada
                 * 6 - Se não foi eleito um taxista para a solicitação, esta é marcada como 'não atendida'
                 */

                try
                {
                    SolicitacaoCorrida solicitacao = null;
                    Log.Information("Buscando dados da solicitação corrida");
                    //using (var scope = parametros.serviceScope.CreateScope())
                    {
                        var solicitacaoCorridaRepo = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                        solicitacao = await solicitacaoCorridaRepo.FindByIdAsync(parametros.IdSolicitacaoCorrida, new[] { "LocalizacaoOrigem" });
                    }

                    Log.Information("Solicitação Corrida após Busca em banco (nulo?): " + (solicitacao is null));

                    if (solicitacao is null)
                    {
                        return;
                    }

                    if (solicitacao.Situacao == SituacaoSolicitacaoCorrida.Indefinido)
                    {
                        // passa a avaliar a solicitação de corrida
                        await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.EmAvaliacao);
                    }

                    IEnumerable<Taxista> taxistasEleitos = null;

                    while (solicitacao.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao)
                    {
                        switch (solicitacao.StatusMonitoramento)
                        {
                            case StatusMonitoramentoSolicitacaoCorrida.Indefinido:
                                {
                                    await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Acumulacao);
                                    continue;
                                }
                            case StatusMonitoramentoSolicitacaoCorrida.Acumulacao:
                                {
                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][acumulação]", solicitacao.Id.ToString()));
                                    Thread.Sleep(parametros.JanelaAcumulacao);

                                    taxistasEleitos = await ClassificarTaxistasEleitos(solicitacao);
                                    await AlterarStatusMonitoramento(solicitacao,
                                        taxistasEleitos.Count() == 0 ?
                                        StatusMonitoramentoSolicitacaoCorrida.Disponibilidade : // nenhum taxista aceitou a solicitacao
                                        StatusMonitoramentoSolicitacaoCorrida.Conclusao // algum taxista aceitou a solicitacao
                                        );

                                    continue;
                                }
                            case StatusMonitoramentoSolicitacaoCorrida.Disponibilidade:
                                {
                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][disponibilidade]", solicitacao.Id.ToString()));
                                    // dentro da janela de disponibilidade (aguardando taxista)

                                    int num_taxistas = 0;

                                    int accumTime = 0;
                                    var currTime = DateTime.Now;

                                    do
                                    {
                                        num_taxistas = await ObterRespostasTaxistas(solicitacao); // operação mais 'barata' que a classificação
                                        if (num_taxistas > 0)
                                        {
                                            taxistasEleitos = await ClassificarTaxistasEleitos(solicitacao);
                                            if (taxistasEleitos.Count() > 0)
                                            {
                                                break;
                                            }
                                        }

                                        Thread.Sleep(500);

                                        accumTime += (DateTime.Now - currTime).Milliseconds;
                                        currTime = DateTime.Now;
                                    }
                                    while (accumTime < parametros.JanelaDisponibilidade);

                                    // countdown atingido ou algum taxista aceitou a solicitação
                                    await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Conclusao);

                                    continue;
                                }

                            case StatusMonitoramentoSolicitacaoCorrida.Conclusao:
                                {
                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][conclusão]", solicitacao.Id.ToString()));
                                    if (taxistasEleitos == null)
                                    {
                                        // caso tenha chegado aqui após uma queda do sistema, por exemplo
                                        taxistasEleitos = await ClassificarTaxistasEleitos(solicitacao);
                                    }

                                    if (taxistasEleitos.Count() > 0)
                                    {
                                        // cria registro de corrida para o primeiro taxista da classificação
                                        await ElegerTaxista(solicitacao, taxistasEleitos.First());

                                        // muda o status da solicitação para aceita
                                        await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.Aceita);
                                    }
                                    else
                                    {
                                        // muda o status da solicitação para não atendida
                                        await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.NaoAtendida);
                                    }

                                    await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Encerramento);
                                    continue;
                                }

                            default:
                            case StatusMonitoramentoSolicitacaoCorrida.Encerramento:
                                {
                                    // caso particular... algum erro no fluxo não levou ao encerramento da solicitação
                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][encerramento]", solicitacao.Id.ToString()));

                                    if (solicitacao.Situacao != SituacaoSolicitacaoCorrida.Aceita && 
                                        solicitacao.Situacao != SituacaoSolicitacaoCorrida.NaoAtendida)
                                    {
                                        // muda o status da solicitação para não atendida
                                        await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.NaoAtendida);
                                    }
                                }
                                break;
                        }

                        Thread.Sleep(200);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(string.Format("ERRO no monitoramento de solicitações de corrida: MSG [{0}] INNER [{0}]", e.Message, e.InnerException));
                }

                Log.Information("Saindo do monitoramento da solicitação (public async Task Executar())");
            }
        }

        IServiceScopeFactory scopeFactory = null;
        public IConfiguration configuration { get; }

        ISolicitacaoCorridaRepository solicitacaoCorridaRepo;
        IUnitOfWork unitOfWork;
        ICorridaService corridaService;
        ITarifaService tarifaService;
        IVeiculoTaxistaService veiculoTaxistaService;

        public PoolSolicitacoesCorrida(
            IServiceScopeFactory _scopeFactory,
            IConfiguration _configuration,
            ISolicitacaoCorridaRepository solicitacaoCorridaRepo,
            IUnitOfWork unitOfWork,
            ICorridaService corridaService,
            ITarifaService tarifaService,
            IVeiculoTaxistaService veiculoTaxistaService
            ) : base()
        {
            scopeFactory = _scopeFactory;
            configuration = _configuration;

            this.solicitacaoCorridaRepo = solicitacaoCorridaRepo;
            this.unitOfWork = unitOfWork;
            this.corridaService = corridaService;
            this.tarifaService = tarifaService;
            this.veiculoTaxistaService = veiculoTaxistaService;
        }


        static async void MonitorarSolicitacaoCorrida(object stateInfo)
        {
            var paramMon = (ParametrosMonitoramento)stateInfo;
            Log.Information(string.Format("Iniciado monitoramento da solicitação de corrida: [{0}]", paramMon.IdSolicitacaoCorrida.ToString()));
            await new MonitorSolicitacaoCorrida(paramMon).Executar();
        }

        private async Task<int> ObterRespostasTaxistas(SolicitacaoCorrida solicitacao)
        {
            return await solicitacaoCorridaRepo.ObterNumeroAceitacoes(solicitacao);
        }

        private async Task<IEnumerable<Taxista>> ClassificarTaxistasEleitos(SolicitacaoCorrida solicitacao)
        {
            return await solicitacaoCorridaRepo.ClassificarTaxistas(solicitacao);
        }

        private async Task<bool> ElegerTaxista(SolicitacaoCorrida solicitacao, Taxista taxista)
        {
            var tarifaVigente = (await tarifaService.GetAll()).FirstOrDefault();

            var veiculoTaxista = veiculoTaxistaService.Search(veicTx => veicTx.IdTaxista == taxista.Id && veicTx.Ativo).FirstOrDefault();

            var corrida = await corridaService.CreateAsync(new CorridaSummary()
            {
                IdSolicitacao = solicitacao.Id,
                IdTaxista = taxista.Id,
                Status = solicitacao.TipoAtendimento == TipoAtendimento.Agendado ? StatusCorrida.Agendada : StatusCorrida.Solicitada,
                IdTarifa = tarifaVigente.Id,
                IdVeiculo = veiculoTaxista.IdVeiculo,
                Inicio = solicitacao.TipoAtendimento == TipoAtendimento.Agendado ? solicitacao.Data : (DateTime?)null
            });

            return await unitOfWork.CommitAsync();
        }

        private async Task<bool> AlterarStatusMonitoramento(SolicitacaoCorrida solicitacao, StatusMonitoramentoSolicitacaoCorrida status)
        {
            return await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, status);
        }

        private async Task<bool> AlterarSituacaoSolicitacao(SolicitacaoCorrida solicitacao, SituacaoSolicitacaoCorrida situacao)
        {
            return await solicitacaoCorridaRepo.AlterarSituacao(solicitacao, situacao);
        }

        public async Task Monitorar(Guid IdSolicitacaoCorrida, int JanelaAcumulacao, int JanelaDisponibilidade)
        {
            /*
             * Fluxo:
             * 
             * 1 - A solicitação de corrida é gerada (capturada pelo trigger de insert);
             * 2 - Inicia a janela de acumulação de taxistas interessados na solicitação (10s)
             * 3 - Findo o perído compreendido no passo 2, é realizada a filtragem e classificação (ordenação) 
             *     dos taxistas que aceitaram a solicitação, de acordo com os critérios de favoritagem 
             *     e distância da origem da solicitação
             * 4 - Se no passo 2 nenhum taxista se candidatar, inicia a janela de disponibilidade (50s), que
             *     concede a corrida ao primeiro taxista que aceitar solicitação
             * 5 - Se foi eleito um taxista para a solicitação, esta é marcada como 'aceita' e a corrida é gerada
             * 6 - Se não foi eleito um taxista para a solicitação, esta é marcada como 'não atendida'
             */

            try
            {
                SolicitacaoCorrida solicitacao = null;
                Log.Information("Buscando dados da solicitação corrida");

                solicitacao = await solicitacaoCorridaRepo.FindByIdAsync(IdSolicitacaoCorrida, new[] { "LocalizacaoOrigem" });

                Log.Information("Solicitação Corrida após Busca em banco (nulo?): " + (solicitacao is null));

                if (solicitacao is null)
                {
                    return;
                }

                if (solicitacao.Situacao == SituacaoSolicitacaoCorrida.Indefinido)
                {
                    // passa a avaliar a solicitação de corrida
                    await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.EmAvaliacao);
                }

                IEnumerable<Taxista> taxistasEleitos = null;

                while (solicitacao.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao)
                {
                    switch (solicitacao.StatusMonitoramento)
                    {
                        case StatusMonitoramentoSolicitacaoCorrida.Indefinido:
                            {
                                await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Acumulacao);
                                continue;
                            }
                        case StatusMonitoramentoSolicitacaoCorrida.Acumulacao:
                            {
                                Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][acumulação]", solicitacao.Id.ToString()));
                                Thread.Sleep(JanelaAcumulacao);

                                taxistasEleitos = await ClassificarTaxistasEleitos(solicitacao);
                                await AlterarStatusMonitoramento(solicitacao,
                                    taxistasEleitos.Count() == 0 ?
                                    StatusMonitoramentoSolicitacaoCorrida.Disponibilidade : // nenhum taxista aceitou a solicitacao
                                    StatusMonitoramentoSolicitacaoCorrida.Conclusao // algum taxista aceitou a solicitacao
                                    );

                                continue;
                            }
                        case StatusMonitoramentoSolicitacaoCorrida.Disponibilidade:
                            {
                                Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][disponibilidade]", solicitacao.Id.ToString()));
                                // dentro da janela de disponibilidade (aguardando taxista)

                                int num_taxistas = 0;

                                int accumTime = 0;
                                var currTime = DateTime.Now;

                                do
                                {
                                    num_taxistas = await ObterRespostasTaxistas(solicitacao); // operação mais 'barata' que a classificação
                                    if (num_taxistas > 0)
                                    {
                                        taxistasEleitos = await ClassificarTaxistasEleitos(solicitacao);
                                        if (taxistasEleitos.Count() > 0)
                                        {
                                            break;
                                        }
                                    }

                                    Thread.Sleep(500);

                                    accumTime += (DateTime.Now - currTime).Milliseconds;
                                    currTime = DateTime.Now;
                                }
                                while (accumTime < JanelaDisponibilidade);

                                // countdown atingido ou algum taxista aceitou a solicitação
                                await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Conclusao);

                                continue;
                            }

                        case StatusMonitoramentoSolicitacaoCorrida.Conclusao:
                            {
                                Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][conclusão]", solicitacao.Id.ToString()));
                                if (taxistasEleitos == null)
                                {
                                    // caso tenha chegado aqui após uma queda do sistema, por exemplo
                                    taxistasEleitos = await ClassificarTaxistasEleitos(solicitacao);
                                }

                                if (taxistasEleitos.Count() > 0)
                                {
                                    // cria registro de corrida para o primeiro taxista da classificação
                                    await ElegerTaxista(solicitacao, taxistasEleitos.First());

                                    // muda o status da solicitação para aceita
                                    await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.Aceita);
                                }
                                else
                                {
                                    // muda o status da solicitação para não atendida
                                    await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.NaoAtendida);
                                }

                                await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Encerramento);
                                continue;
                            }

                        default:
                        case StatusMonitoramentoSolicitacaoCorrida.Encerramento:
                            {
                                // caso particular... algum erro no fluxo não levou ao encerramento da solicitação
                                Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][encerramento]", solicitacao.Id.ToString()));

                                if (solicitacao.Situacao != SituacaoSolicitacaoCorrida.Aceita &&
                                    solicitacao.Situacao != SituacaoSolicitacaoCorrida.NaoAtendida)
                                {
                                    // muda o status da solicitação para não atendida
                                    await AlterarSituacaoSolicitacao(solicitacao, SituacaoSolicitacaoCorrida.NaoAtendida);
                                }
                            }
                            break;
                    }

                    Thread.Sleep(200);
                }
            }
            catch (Exception e)
            {
                Log.Error(string.Format("ERRO no monitoramento de solicitações de corrida: MSG [{0}] INNER [{0}]", e.Message, e.InnerException));
            }

            Log.Information("Saindo do monitoramento da solicitação (public async Task Executar())");
        }

        private void Inicializar()
        {
            Log.Information("Iniciando monitoramento de solicitações de corrida...");
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();

                    Log.Information("   Carregando solicitações vigentes...");
                    // obtém solicitações vigentes
                    var solicitacoes = solicitacaoCorridaRepo.Search(
                        sol_corrida =>
                            sol_corrida.Situacao == SituacaoSolicitacaoCorrida.Indefinido ||
                            sol_corrida.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao,
                        new[] { "Passageiro", "Passageiro.TaxistasFavoritos" });

                    Log.Information(string.Format("   {0} solicitações em andamento!", solicitacoes.Count()));

                    foreach (var solicitacao in solicitacoes)
                    {
                        Task.Factory.StartNew(async () =>
                        {
                            await this.Monitorar(
                                solicitacao.Id,
                                configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaAcumulacao"),
                                configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaDisponibilidade")
                            );
                        });

                        /*ThreadPool.QueueUserWorkItem(MonitorarSolicitacaoCorrida, new ParametrosMonitoramento()
                        {
                            IdSolicitacaoCorrida = solicitacao.Id,
                            JanelaAcumulacao = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaAcumulacao"),
                            JanelaDisponibilidade = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaDisponibilidade"),
                            serviceScope = scopeFactory.CreateScope()
                        });*/
                    }

                    // registra nos triggers de solicitação de corrida
                    Triggers<SolicitacaoCorrida>.Inserted += insertingEntry =>
                    {
                        Log.Information(string.Format("Nova solicitação de corrida lançada: {0}", insertingEntry.Entity.Id));
                        // nova solicitação
                        Task.Factory.StartNew(async () =>
                        {
                            await this.Monitorar(
                                insertingEntry.Entity.Id,
                                configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaAcumulacao"),
                                configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaDisponibilidade")
                            );
                        });

                        /*ThreadPool.QueueUserWorkItem(MonitorarSolicitacaoCorrida, new ParametrosMonitoramento()
                        {
                            IdSolicitacaoCorrida = insertingEntry.Entity.Id,
                            JanelaAcumulacao = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaAcumulacao"),
                            JanelaDisponibilidade = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaDisponibilidade"),
                            serviceScope = scopeFactory.CreateScope()
                        });*/
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + " | " + ex.StackTrace);
            }
            Log.Information("Monitoramento de solicitações de corridas iniciado");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Inicializar();

            // obtém as solicitações de corrida
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(200, stoppingToken);
            }

            await Task.CompletedTask;

            Log.Information("Saindo do monitoramento da solicitação (protected override async Task ExecuteAsync(CancellationToken stoppingToken))");
        }
    }
}
