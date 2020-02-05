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
using Serilog;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;

namespace CloudMe.MotoTEX.Domain.Services.Background
{
    public class PoolSolicitacoesCorrida : BackgroundService
    {
        private class ParametrosMonitoramento
        {
            public Guid IdSolicitacaoCorrida { get; set; }
            public IServiceProvider serviceProvider { get; set; }
            public int JanelaFaixaAtivacao { get; set; } = 10000; // default = 10s
            public int JanelaDisponibilidade { get; set; } = 50000; // default = 50s
        }

        private class MonitorSolicitacaoCorrida
        {
            ParametrosMonitoramento parametros { get; set; }

            IConfiguration Configuration;

            ISolicitacaoCorridaRepository solicitacaoCorridaRepo;
            ITaxistaRepository taxistaRepo;
            IUnitOfWork unitOfWork;
            ICorridaService corridaService;
            ITarifaService tarifaService;
            IVeiculoTaxistaService veiculoTaxistaService;

            ITaxistaService taxistaService;
            IFavoritoService favoritoService;
            ISolicitacaoCorridaService solicitacaoCorridaService;
            IProxyNotificacoesSolicitacaoCorrida notificacoesSolicitacaoCorrida;

            List<FaixaAtivacao> FaixasAtivacao = new List<FaixaAtivacao>();


            public MonitorSolicitacaoCorrida(ParametrosMonitoramento parametros)
            {
                this.parametros = parametros;
                var serviceScope = parametros.serviceProvider.CreateScope();

                solicitacaoCorridaRepo = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();
                taxistaRepo = serviceScope.ServiceProvider.GetRequiredService<ITaxistaRepository>();
                unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                corridaService = serviceScope.ServiceProvider.GetRequiredService<ICorridaService>();
                tarifaService = serviceScope.ServiceProvider.GetRequiredService<ITarifaService>();
                veiculoTaxistaService = serviceScope.ServiceProvider.GetRequiredService<IVeiculoTaxistaService>();

                taxistaService = serviceScope.ServiceProvider.GetRequiredService<ITaxistaService>();
                favoritoService = serviceScope.ServiceProvider.GetRequiredService<IFavoritoService>();
                Configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
                solicitacaoCorridaService = serviceScope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaService>();
                notificacoesSolicitacaoCorrida = serviceScope.ServiceProvider.GetRequiredService<IProxyNotificacoesSolicitacaoCorrida>();

                Configuration.GetSection("FaixasAtivacaoSolicitacaoCorrida").GetChildren().ToList().ForEach(x =>
                {
                    FaixasAtivacao.Add(new FaixaAtivacao()
                    {
                        RaioInicial = Convert.ToDouble(x.GetSection("RaioInicial").Value),
                        Janela = Convert.ToInt32(x.GetSection("Janela").Value),
                        RaioFinal = Convert.ToDouble(x.GetSection("RaioFinal").Value),
                    });
                });
            }

            private async Task<IEnumerable<Taxista>> ClassificarTaxistasInteressados(SolicitacaoCorrida solicitacao)
            {
                var taxistas = await taxistaRepo.Search(
                    taxista =>
                        taxista.SolicitacoesCorrida.Any(
                            solCorrTx => solCorrTx.IdSolicitacaoCorrida == solicitacao.Id &&
                            solCorrTx.Acao == AcaoTaxistaSolicitacaoCorrida.Aceita) // ... que participou do pregão da solicitação
                , new[] { "SolicitacoesCorrida" });

                var favoritos = await favoritoService.Search(fav => fav.IdPassageiro == solicitacao.IdPassageiro);

                var taxistas_por_distancia_com_favoritos =
                    from tx in taxistas
                    join favorito in favoritos on tx.Id equals favorito.IdTaxista into tx_fav_join
                    from tx_fav in tx_fav_join.DefaultIfEmpty()
                    select new
                    {
                        taxista = tx,
                        distancia = Localizacao.ObterDistancia(solicitacao.LocalizacaoOrigem, tx.LocalizacaoAtual),
                        pref_favorito = tx_fav != null ? tx_fav.Preferencia : int.MaxValue
                    };

                var resultado =
                    from tx_fav in taxistas_por_distancia_com_favoritos
                    orderby tx_fav.pref_favorito, tx_fav.distancia
                    select tx_fav.taxista;

                return resultado;
            }

            private async Task<bool> ElegerTaxista(SolicitacaoCorrida solicitacao, Taxista taxista)
            {
                var tarifaVigente = (await tarifaService.GetAll()).FirstOrDefault();

                var veiculoTaxista = (await veiculoTaxistaService.Search(veicTx => veicTx.IdTaxista == taxista.Id && veicTx.Ativo)).FirstOrDefault();

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

                    var retries = 10;
                    do
                    {
                        solicitacao = await solicitacaoCorridaRepo.FindByIdAsync(parametros.IdSolicitacaoCorrida, new[] { "LocalizacaoOrigem" });
                        if (solicitacao is null)
                        {
                            retries--;
                        }
                        else
                        {
                            retries = 0;
                        }
                        Log.Information(string.Format("Solicitação Corrida após Busca em banco (nulo?) - Tentativas restantes {0}: {1}", retries.ToString(), (solicitacao is null).ToString()));
                        Thread.Sleep(500);
                    }
                    while (retries > 0);

                    if (solicitacao is null)
                    {
                        return;
                    }

                    if (solicitacao.Situacao == SituacaoSolicitacaoCorrida.Indefinido)
                    {
                        // passa a avaliar a solicitação de corrida
                        await solicitacaoCorridaRepo.AlterarSituacao(solicitacao, SituacaoSolicitacaoCorrida.EmAvaliacao);
                    }

                    IEnumerable<Taxista> taxistasInteressados = null;

                    while (solicitacao.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao)
                    {
                        switch (solicitacao.StatusMonitoramento)
                        {
                            case StatusMonitoramentoSolicitacaoCorrida.Indefinido:
                                {
                                    await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Ativacao);
                                    continue;
                                }
                            case StatusMonitoramentoSolicitacaoCorrida.Ativacao:
                                {
                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][ativação]", solicitacao.Id.ToString()));
                                    // itera sobre os raios de abrangência até encontrar um taxista que aceite a solicitação

                                    // Obs.: A faixa livre (após a última faixa de ativação) é a do índice 'FaixasAtivacao.Length'
                                    // Assim, não retirar o operador <= do laço

                                    bool solicitacaoAceita = false;

                                    for (var idxFxAtivacao = solicitacao.IdxFaixaBusca; idxFxAtivacao <= FaixasAtivacao.Count; ++idxFxAtivacao)
                                    {
                                        var faixaLivre = idxFxAtivacao == FaixasAtivacao.Count;

                                        await solicitacaoCorridaRepo.AlterarFaixaAtivacao(solicitacao, idxFxAtivacao);

                                        double? raioInicio = faixaLivre ?
                                            FaixasAtivacao[idxFxAtivacao - 1].RaioFinal :
                                            FaixasAtivacao[idxFxAtivacao].RaioInicial;

                                        double? raioFim = faixaLivre ?
                                            (double?)null :
                                            FaixasAtivacao[idxFxAtivacao].RaioFinal;

                                        var taxistasFaixa = (await taxistaService.ProcurarPorDistancia(
                                            new Model.Localizacao.LocalizacaoSummary() {Endereco = solicitacao.LocalizacaoOrigem.Endereco, Id = solicitacao.LocalizacaoOrigem.Id, IdUsuario = solicitacao.LocalizacaoOrigem.IdUsuario, Latitude = solicitacao.LocalizacaoOrigem.Latitude, Longitude = solicitacao.LocalizacaoOrigem.Longitude, NomePublico = solicitacao.LocalizacaoOrigem.NomePublico },
                                            raioInicio, raioFim, new[] { "FormasPagamento", "FaixasDesconto" }))
                                            .Where(taxista =>
                                            (taxista.FormasPagamento.Any(frmPgto => frmPgto.IdFormaPagamento == solicitacao.IdFormaPagamento)) && // ... que aceita a forma de pagamento da solicitação
                                            (taxista.FaixasDesconto.Any(fxDesc => fxDesc.IdFaixaDesconto == solicitacao.IdFaixaDesconto) || !solicitacao.IdFaixaDesconto.HasValue) // ... que adota a faixa de desconto solicitada
                                        );

                                        if (taxistasFaixa.Count() == 0)
                                        {
                                            // vai pra próxima faixa
                                            continue;
                                        }

                                        // apresenta a solicitação aos taxistas da faixa atual
                                        await notificacoesSolicitacaoCorrida.AtivarTaxistas(
                                            taxistasFaixa,
                                            await solicitacaoCorridaService.GetSummaryAsync(parametros.IdSolicitacaoCorrida));

                                        // inicia o timeout da faixa de ativação
                                        Thread.Sleep(parametros.JanelaFaixaAtivacao);

                                        // verifica se algum taxista atendeu a solicitação na faixa
                                        var numAceitacoes = await solicitacaoCorridaRepo.ObterNumeroAceitacoes(solicitacao);

                                        if (numAceitacoes > 0)
                                        {
                                            await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Eleicao); // algum taxista aceitou a solicitacao

                                            solicitacaoAceita = true;
                                            break;
                                        }
                                    }

                                    //await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Disponibilidade); // nenhum taxista aceitou a solicitacao

                                    if (!solicitacaoAceita)
                                    {
                                        await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Encerramento); // nenhum taxista aceitou a solicitacao
                                    }
                                    continue;
                                }
                            /*case StatusMonitoramentoSolicitacaoCorrida.Disponibilidade:
                                {
                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][disponibilidade]", solicitacao.Id.ToString()));
                                    // dentro da janela de disponibilidade (aguardando qualquer taxista ativado na fase anterior)

                                    int accumTime = 0;
                                    var currTime = DateTime.Now;

                                    do
                                    {
                                        var numAceitacoes = await solicitacaoCorridaRepo.ObterNumeroAceitacoes(solicitacao); // operação mais 'barata' que a classificação
                                        if (numAceitacoes > 0)
                                        {
                                            // alguem aceitou a corrida
                                            await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Eleicao);
                                            break;
                                        }

                                        Thread.Sleep(100);

                                        accumTime += (DateTime.Now - currTime).Milliseconds;
                                        currTime = DateTime.Now;
                                    }
                                    while (accumTime < parametros.JanelaDisponibilidade);

                                    // countdown atingido
                                    await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Encerramento);

                                    continue;
                                }*/

                            case StatusMonitoramentoSolicitacaoCorrida.Eleicao:
                                {
                                    // Alguém aceitou a solicitação de corrida em alguma das fases anteriores (ativação ou disponibilidade)

                                    Log.Information(string.Format("Status monitoramento da solicitação de corrida: [{0}][conclusão]", solicitacao.Id.ToString()));

                                    // recupera a lista de interessados, caso tenha sido perdida por algum motivo (queda do sistema, por exemplo)
                                    taxistasInteressados = await ClassificarTaxistasInteressados(solicitacao);

                                    if (taxistasInteressados.Count() > 0)
                                    {
                                        // cria registro de corrida para o primeiro taxista da classificação
                                        if (await ElegerTaxista(solicitacao, taxistasInteressados.First()))
                                        {
                                            // muda o status da solicitação para aceita
                                            await solicitacaoCorridaRepo.AlterarSituacao(solicitacao, SituacaoSolicitacaoCorrida.Aceita);
                                        }

                                    }
                                    else
                                    {
                                        // muda o status da solicitação para não atendida
                                        await solicitacaoCorridaRepo.AlterarSituacao(solicitacao, SituacaoSolicitacaoCorrida.NaoAtendida);
                                    }

                                    await solicitacaoCorridaRepo.AlterarStatusMonitoramento(solicitacao, StatusMonitoramentoSolicitacaoCorrida.Encerramento);
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
                                        await solicitacaoCorridaRepo.AlterarSituacao(solicitacao, SituacaoSolicitacaoCorrida.NaoAtendida);
                                    }
                                }
                                break;
                        }

                        Thread.Sleep(100);
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
        IServiceProvider serviceProvider;

        public PoolSolicitacoesCorrida(
            IServiceScopeFactory _scopeFactory,
            IConfiguration _configuration,
            IServiceProvider serviceProvider
            ) : base()
        {
            scopeFactory = _scopeFactory;
            configuration = _configuration;
            this.serviceProvider = serviceProvider;
        }


        static async void MonitorarSolicitacaoCorrida(object stateInfo)
        {
            var paramMon = (ParametrosMonitoramento)stateInfo;
            Log.Information(string.Format("Iniciado monitoramento da solicitação de corrida: [{0}]", paramMon.IdSolicitacaoCorrida.ToString()));
            await new MonitorSolicitacaoCorrida(paramMon).Executar();
        }

        private async Task Inicializar()
        {
            Log.Information("Iniciando monitoramento de solicitações de corrida...");
            try
            {
                var janelaAtivacao = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaFaixaAtivacao");
                var janelaAcumulacao = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaAcumulacao");
                var janelaDisponibilidade = configuration.GetValue<int>("MonitorSolicitacoesCorrida:JanelaAcumulacao");

                using (var scope = scopeFactory.CreateScope())
                {
                    var solicitacaoCorridaRepo = scope.ServiceProvider.GetRequiredService<ISolicitacaoCorridaRepository>();

                    Log.Information("   Carregando solicitações vigentes...");
                    // obtém solicitações vigentes
                    var idsSolicitacoes = (await solicitacaoCorridaRepo.Search(
                        sol_corrida =>
                            sol_corrida.Situacao == SituacaoSolicitacaoCorrida.Indefinido ||
                            sol_corrida.Situacao == SituacaoSolicitacaoCorrida.EmAvaliacao)).Select(x => x.Id);

                    Log.Information(string.Format("   {0} solicitações em andamento!", idsSolicitacoes.Count()));

                    foreach (var idSolicitacao in idsSolicitacoes)
                    {
                        await Task.Factory.StartNew(async () =>
                        {
                            Log.Information(string.Format("Iniciado monitoramento da solicitação de corrida: [{0}]", idSolicitacao));
                            await new MonitorSolicitacaoCorrida(new ParametrosMonitoramento
                            {
                                IdSolicitacaoCorrida = idSolicitacao,
                                JanelaFaixaAtivacao = janelaAtivacao,
                                JanelaDisponibilidade = janelaDisponibilidade,
                                serviceProvider = serviceProvider
                            }).Executar();
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
                            Log.Information(string.Format("Iniciado monitoramento da solicitação de corrida: [{0}]", insertingEntry.Entity.Id));
                            await new MonitorSolicitacaoCorrida(new ParametrosMonitoramento
                            {
                                IdSolicitacaoCorrida = insertingEntry.Entity.Id,
                                JanelaFaixaAtivacao = janelaAtivacao,
                                JanelaDisponibilidade = janelaDisponibilidade,
                                serviceProvider = serviceProvider
                            }).Executar();
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
            await Inicializar();

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
