using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CloudMe.MotoTEX.Configuration.Library.Constants;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using CloudMe.MotoTEX.Domain.Services;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Notifications;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using CloudMe.MotoTEX.Domain.Model.Foto;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using Microsoft.AspNetCore.SignalR;
using CloudMe.MotoTEX.Configuration.Library.SignalR;
using EntityFrameworkCore.Triggers;
using EntityFrameworkCore.Triggers.AspNetCore;
using CloudMe.MotoTEX.Domain.Services.Background;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using Microsoft.Extensions.Hosting;
using CloudMe.MotoTEX.Domain.Notifications.Abstracts;
using CloudMe.MotoTEX.Domain.Notifications.Abstract;
using CloudMe.MotoTEX.Domain.Model;
using CloudMe.MotoTEX.Domain.Model.Faturamento;

namespace CloudMe.MotoTEX.Configuration.Library.Helpers
{
    public static class StartupHelpers
    {
        public static object CORSDefaults { get; private set; }

        public static IServiceCollection AddDbContexts<TContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext
        {

            var migrationsAssembly = typeof(TContext).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString(ConfigurationConsts.AdminConnectionStringKey);

            services.AddSingleton<OperationalStoreOptions>();
            services.AddSingleton<ConfigurationStoreOptions>();
            services.AddDbContext<TContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));
            return services;
        }

        public static IServiceCollection AddMotoTEXServices(this IServiceCollection services)
        {
            services.AddTransient<ICorridaService, CorridaService>();
            services.AddTransient<IFaixaDescontoService, FaixaDescontoService>();
            services.AddTransient<IFaixaDescontoTaxistaService, FaixaDescontoTaxistaService>();
            services.AddTransient<IFavoritoService, FavoritoService>();
            services.AddTransient<IFormaPagamentoService, FormaPagamentoService>();
            services.AddTransient<IFormaPagamentoTaxistaService, FormaPagamentoTaxistaService>();
            services.AddTransient<IGrupoUsuarioService, GrupoUsuarioService>();
            services.AddTransient<ILocalizacaoService, LocalizacaoService>();
            services.AddTransient<IEnderecoService, EnderecoService>();
            services.AddTransient<IPassageiroService, PassageiroService>();
            services.AddTransient<IRotaService, RotaService>();
            services.AddTransient<ISolicitacaoCorridaService, SolicitacaoCorridaService>();
            services.AddTransient<ITarifaService, TarifaService>();
            services.AddTransient<ITaxistaService, TaxistaService>();
            services.AddTransient<IPontoTaxiService, PontoTaxiService>();
            services.AddTransient<IUsuarioGrupoUsuarioService, UsuarioGrupoUsuarioService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IVeiculoService, VeiculoService>();
            services.AddTransient<IVeiculoTaxistaService, VeiculoTaxistaService>();
            services.AddTransient<IFotoService, FotoService>();
            services.AddTransient<ICorVeiculoService, CorVeiculoService>();
            services.AddTransient<IContratoService, ContratoService>();
            services.AddTransient<IContatoService, ContatoService>();
            services.AddTransient<IMensagemService, MensagemService>();
            services.AddTransient<IEmergenciaService, EmergenciaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(p => p.GetService<IHttpContextAccessor>()?.HttpContext);

            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddSingleton<IPoolLocalizacaoTaxista, PoolLocalizacaoTaxista>();
            services.AddHostedService<PoolLocalizacaoTaxista>();

            services.AddHostedService<PoolLocalizacaoPassageiro>();

            services.AddHostedService<PoolSolicitacoesCorrida>();
            services.AddHostedService<PoolCorridas>();

            new MonitorGruposUsuarios();

            services.AddTransient<IProxyHubMensagens, ProxyHubMensagens>();

            services.AddTriggers();

            return services;
        }
        public static IServiceCollection AddMotoTEXRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICorridaRepository, CorridaRepository>();
            services.AddTransient<IFaixaDescontoRepository, FaixaDescontoRepository>();
            services.AddTransient<IFaixaDescontoTaxistaRepository, FaixaDescontoTaxistaRepository>();
            services.AddTransient<IFavoritoRepository, FavoritoRepository>();
            services.AddTransient<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddTransient<IFormaPagamentoTaxistaRepository, FormaPagamentoTaxistaRepository>();
            services.AddTransient<IGrupoUsuarioRepository, GrupoUsuarioRepository>();
            services.AddTransient<ILocalizacaoRepository, LocalizacaoRepository>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddTransient<IPassageiroRepository, PassageiroRepository>();
            services.AddTransient<IRotaRepository, RotaRepository>();
            services.AddTransient<ISolicitacaoCorridaRepository, SolicitacaoCorridaRepository>();
            services.AddTransient<ITarifaRepository, TarifaRepository>();
            services.AddTransient<ITaxistaRepository, TaxistaRepository>();
            services.AddTransient<IPontoTaxiRepository, PontoTaxiRepository>();
            services.AddTransient<IUsuarioGrupoUsuarioRepository, UsuarioGrupoUsuarioRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IVeiculoRepository, VeiculoRepository>();
            services.AddTransient<IVeiculoTaxistaRepository, VeiculoTaxistaRepository>();
            services.AddTransient<IFotoRepository, FotoRepository>();
            services.AddTransient<ICorVeiculoRepository, CorVeiculoRepository>();
            services.AddTransient<IContratoRepository, ContratoRepository>();
            services.AddTransient<IContatoRepository, ContatoRepository>();
            services.AddTransient<IMensagemRepository, MensagemRepository>();
            services.AddTransient<IMensagemDestinatarioRepository, MensagemDestinatarioRepository>();
            services.AddTransient<IEmergenciaRepository, EmergenciaRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static async void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CloudMeMotoTEXContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            //services.AddAuthorization();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
                    policy => policy.RequireRole(AuthorizationConsts.AdministrationRole));
            });
        }

        public static void ConfigureSignalR<TContext>(IApplicationBuilder app) where TContext: DbContext
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<EntityNotifier<ICorridaService, Corrida, CorridaSummary, Guid>>("/notifications/corrida");
                routes.MapHub<EntityNotifier<ICorVeiculoService, CorVeiculo, CorVeiculoSummary, Guid>>("/notifications/cor_veiculo");
                routes.MapHub<EntityNotifier<IEnderecoService, Endereco, EnderecoSummary, Guid>>("/notifications/endereco");
                routes.MapHub<EntityNotifier<IFaixaDescontoService, FaixaDesconto, FaixaDescontoSummary, Guid>>("/notifications/faixa_desconto");
                routes.MapHub<EntityNotifier<IFaixaDescontoTaxistaService, FaixaDescontoTaxista, FaixaDescontoTaxistaSummary, Guid>>("/notifications/faixa_desconto_taxista");
                routes.MapHub<EntityNotifier<IFavoritoService, Favorito, FavoritoSummary, Guid>>("/notifications/favorito");
                routes.MapHub<EntityNotifier<IFormaPagamentoService, FormaPagamento, FormaPagamentoSummary, Guid>>("/notifications/forma_pagamento");
                routes.MapHub<EntityNotifier<IFormaPagamentoTaxistaService, FormaPagamentoTaxista, FormaPagamentoTaxistaSummary, Guid>>("/notifications/forma_pagamento_taxista");
                routes.MapHub<EntityNotifier<IFotoService, Foto, FotoSummary, Guid>>("/notifications/foto");
                routes.MapHub<UsuarioNotifier>("/notifications/usuario");
                routes.MapHub<EntityNotifier<IGrupoUsuarioService, GrupoUsuario, GrupoUsuarioSummary, Guid>>("/notifications/grupo_usuario");
                routes.MapHub<EntityNotifier<ILocalizacaoService, Localizacao, LocalizacaoSummary, Guid>>("/notifications/localizacao");
                routes.MapHub<EntityNotifier<IPassageiroService, Passageiro, PassageiroSummary, Guid>>("/notifications/passageiro");
                routes.MapHub<EntityNotifier<IPontoTaxiService, PontoTaxi, PontoTaxiSummary, Guid>>("/notifications/ponto_taxi");
                routes.MapHub<EntityNotifier<IRotaService, Rota, RotaSummary, Guid>>("/notifications/rota");
                routes.MapHub<EntityNotifier<ISolicitacaoCorridaService, SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>>("/notifications/solicitacao_corrida");
                routes.MapHub<EntityNotifier<ITarifaService, Tarifa, TarifaSummary, Guid>>("/notifications/tarifa");
                routes.MapHub<EntityNotifier<ITaxistaService, Taxista, TaxistaSummary, Guid>>("/notifications/taxista");
                routes.MapHub<EntityNotifier<IUsuarioGrupoUsuarioService, UsuarioGrupoUsuario, UsuarioGrupoUsuarioSummary, Guid>>("/notifications/usuario_grupo_usuario");
                routes.MapHub<EntityNotifier<IVeiculoService, Veiculo, VeiculoSummary, Guid>>("/notifications/veiculo");
                routes.MapHub<EntityNotifier<IVeiculoTaxistaService, VeiculoTaxista, VeiculoTaxistaSummary, Guid>>("/notifications/veiculo_taxista");
                routes.MapHub<EntityNotifier<IContatoService, Contato, ContatoSummary, Guid>>("/notifications/contato");
                routes.MapHub<EntityNotifier<IFaturamentoService, Faturamento, FaturamentoSummary, Guid>>("/notifications/Faturamento");
                routes.MapHub<EntityNotifier<IFaturamentoTaxistaService, FaturamentoTaxista, FaturamentoTaxistaSummary, Guid>>("/notifications/FaturamentoTaxista");

                routes.MapHub<HubLocalizacaoTaxista>("/notifications/localizacao_taxista");
                routes.MapHub<HubLocalizacaoPassageiro>("/notifications/localizacao_passageiro");

                routes.MapHub<HubMensagens>("/notifications/mensagens");
            });

            BuildEntryNotifiers<TContext>(app);
        }

        public static void BuildEntryNotifiers<TContext>(IApplicationBuilder app) where TContext : DbContext
        {
            app.UseTriggers(buider =>
            {
                buider.Triggers<Localizacao>().Inserted.Add(entry =>
                {
                    Console.Write("Updated: [{0}]", entry.Entity.Id);
                });
            });
        }
    }
}
