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

namespace CloudMe.ToDeTaxi.Domain.Services.Background
{
    public class PoolSolicitacoesCorrida : BackgroundService
    {
        private class ParametrosMonitoramento
        {
            public Guid IdSolicitacaoCorrida { get; set; }
            public IServiceScopeFactory ScopeFactory { get; set; }
            public int JanelaAcumulacao { get; set; } = 10000; // default = 10s
            public int JanelaDisponibilidade { get; set; } = 50000; // default = 50s
        }

        private class MonitorSolicitacaoCorrida
        {
            ParametrosMonitoramento parametros { get; set; }

            public MonitorSolicitacaoCorrida(ParametrosMonitoramento parametros)
            {
                this.parametros = parametros;
            }

            private async Task<int> ObterRespostasTaxistas(SolicitacaoCorrida solicitacao)
            {
                using (var scope = parametros.ScopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.ObterNumeroAceitacoes(solicitacao);
                }
            }

            private async Task<IEnumerable<Taxista>> ClassificarTaxistasEleitos(SolicitacaoCorrida solicitacao)
            {
                using (var scope = parametros.ScopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.ClassificarTaxistas(solicitacao);
                }
            }

            private async Task<bool> ElegerTaxista(SolicitacaoCorrida solicitacao, Taxista taxista)
            {
                using (var scope = parametros.ScopeFactory.CreateScope())
                {
                    var corridaService = scope.ServiceProvider.GetRequiredService<ICorridaService>();
                    var tarifaService = scope.ServiceProvider.GetRequiredService<ITarifaService>();
                    var veiculoTaxistaService = scope.ServiceProvider.GetRequiredService<IVeiculoTaxistaService>();

                    var tarifaVigente = (await tarifaService.GetAll()).FirstOrDefault();

                    var veiculoTaxista = veiculoTaxistaService.Search(veicTx => veicTx.IdTaxista == taxista.Id).FirstOrDefault();

                    var corrida = await corridaService.CreateAsync(new CorridaSummary()
                    {
                        IdSolicitacao = solicitacao.Id,
                        IdTaxista = taxista.Id,
                        Status = StatusCorrida.Solicitada,
                        IdTarifa = tarifaVigente.Id,
                        IdVeiculo = veiculoTaxista.Id
                    });

                    return corrida != null;
                }
            }

            private async Task<bool> AlterarStatusMonitoramento(SolicitacaoCorrida solicitacao, StatusMonitoramentoSolicitacaoCorrida status)
            {
                using (var scope = parametros.ScopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    return await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, status);
                }
            }

            private async Task<bool> AlterarSituacaoSolicitacao(SolicitacaoCorrida solicitacao, SituacaoSolicitacaoCorrida situacao)
            {
                using (var scope = parametros.ScopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
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

                SolicitacaoCorrida solicitacao = null;
                using (var scope = parametros.ScopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                    solicitacao = await solicitacaoCorridaRepo.FindByIdAsync(parametros.IdSolicitacaoCorrida);
                }

                if (solicitacao is null)
                {
                    return;
                }

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
                                Thread.Sleep(parametros.JanelaAcumulacao);

                                var num_taxistas = await ObterRespostasTaxistas(solicitacao);
                                await AlterarStatusMonitoramento(solicitacao,
                                    num_taxistas == 0 ?
                                    StatusMonitoramentoSolicitacaoCorrida.Disponibilidade : // nenhum taxista aceitou a solicitacao
                                    StatusMonitoramentoSolicitacaoCorrida.Conclusao // algum taxista aceitou a solicitacao
                                    );

                                continue;
                            }
                        case StatusMonitoramentoSolicitacaoCorrida.Disponibilidade:
                            {
                                // dentro da janela de disponibilidade (aguardando taxista)

                                int num_taxistas = 0;

                                int accumTime = 0;
                                var currTime = DateTime.Now;

                                do
                                {
                                    num_taxistas = await ObterRespostasTaxistas(solicitacao);
                                    Thread.Sleep(500);

                                    accumTime += (DateTime.Now - currTime).Seconds;
                                    currTime = DateTime.Now;
                                }
                                while (accumTime < parametros.JanelaDisponibilidade || num_taxistas == 0);

                                // countdown atingido ou algum taxista aceitou a solicitação
                                await AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Conclusao);

                                continue;
                            }

                        case StatusMonitoramentoSolicitacaoCorrida.Conclusao:
                            {
                                var taxistas = await ClassificarTaxistasEleitos(solicitacao);
                                if (taxistas.Count() > 0)
                                {
                                    // cria registro de corrida para o primeiro taxista da classificação
                                    await ElegerTaxista(solicitacao, taxistas.First());

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

                        case StatusMonitoramentoSolicitacaoCorrida.Encerramento:
                            {

                            }
                            break;
                        default:
                            break;
                    }

                    Thread.Sleep(200);
                }
            }
        }

        IServiceScopeFactory scopeFactory = null;
        public IConfiguration configuration { get; }

        public PoolSolicitacoesCorrida(IServiceScopeFactory _scopeFactory, IConfiguration _configuration) : base()
        {
            scopeFactory = _scopeFactory;
            configuration = _configuration;
        }


        static async void MonitorarSolicitacaoCorrida(object stateInfo)
        {
            await new MonitorSolicitacaoCorrida((ParametrosMonitoramento)stateInfo).Executar();
        }

        private void Inicializar()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                
                // obtém solicitações vigentes
                var solicitacoes = solicitacaoCorridaRepo.Search(
                    sol_corrida => sol_corrida.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao,
                    new[] { "Passageiro", "Passageiro.TaxistasFavoritos" });

                foreach (var solicitacao in solicitacoes)
                {
                    ThreadPool.QueueUserWorkItem(MonitorarSolicitacaoCorrida, new ParametrosMonitoramento()
                    {
                        IdSolicitacaoCorrida = solicitacao.Id,
                        JanelaAcumulacao = configuration.GetValue<int>("MonitorSolicitacoesCorrida.JanelaAcumulacao"),
                        JanelaDisponibilidade = configuration.GetValue<int>("MonitorSolicitacoesCorrida.JanelaDisponibilidade"),
                        ScopeFactory = scopeFactory
                    });
                }

                // registra nos triggers de solicitação de corrida
                Triggers<SolicitacaoCorrida>.Inserted += insertingEntry =>
                {
                    // nova solicitação
                    ThreadPool.QueueUserWorkItem(MonitorarSolicitacaoCorrida, new ParametrosMonitoramento()
                    {
                        IdSolicitacaoCorrida = insertingEntry.Entity.Id,
                        JanelaAcumulacao = configuration.GetValue<int>("MonitorSolicitacoesCorrida.JanelaAcumulacao"),
                        JanelaDisponibilidade = configuration.GetValue<int>("MonitorSolicitacoesCorrida.JanelaDisponibilidade"),
                        ScopeFactory = scopeFactory
                    });
                };
            }
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
        }
    }
}
