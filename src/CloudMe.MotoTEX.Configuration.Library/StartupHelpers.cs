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
using CloudMe.MotoTEX.Domain.Notifications.Abstract;
using CloudMe.MotoTEX.Domain.Model;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Proxies;
using CloudMe.MotoTEX.Domain.Model.Faturamento;
using CloudMe.MotoTEX.Domain.Notifications.Compat;

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
            services.AddTransient<IFaturamentoService, FaturamentoService>();
            services.AddTransient<IFaturamentoTaxistaService, FaturamentoTaxistaService>();
            services.AddTransient<IFaixaAtivacaoService, FaixaAtivacaoService>();
            services.AddTransient<IUFService, UFService>();
            services.AddTransient<ICidadeService, CidadeService>();
            services.AddTransient<IHabilitacaoService, HabilitacaoService>();
            services.AddTransient<IRegistroVeiculoService, RegistroVeiculoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(p => p.GetService<IHttpContextAccessor>()?.HttpContext);

            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddHostedService<PoolLocalizacaoTaxista>();
            services.AddHostedService<PoolLocalizacaoPassageiro>();
            services.AddHostedService<PoolSolicitacoesCorrida>();
            services.AddHostedService<PoolCorridas>();

            //services.AddSingleton<MonitorGruposUsuarios>();
            new MonitorGruposUsuarios();

            services.AddTransient<IProxyMensagem, ProxyMensagem>();
            services.AddTransient<IProxySolicitacaoCorrida, ProxySolicitacaoCorrida>();
            services.AddTransient<IProxyLocalizacao, ProxyLocalizacao>();
            services.AddTransient<IProxyEmergencia, ProxyEmergencia>();

            services.AddTransient<IFirebaseNotifications, FirebaseNotifications>();

            services.AddTriggers();

            //services.AddSingleton<EntityNotifier<ICorridaService, Corrida, CorridaSummary, Guid>>(); // COMPAT
            new Domain.Notifications.EntityNotifier<ICorVeiculoService, CorVeiculo, CorVeiculoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<ICorVeiculoService, CorVeiculo, CorVeiculoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IEnderecoService, Endereco, EnderecoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFaixaDescontoService, FaixaDesconto, FaixaDescontoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFaixaDescontoTaxistaService, FaixaDescontoTaxista, FaixaDescontoTaxistaSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFavoritoService, Favorito, FavoritoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFormaPagamentoService, FormaPagamento, FormaPagamentoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFormaPagamentoTaxistaService, FormaPagamentoTaxista, FormaPagamentoTaxistaSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFotoService, Foto, FotoSummary, Guid>();
            new UsuarioNotifier();
            new Domain.Notifications.EntityNotifier<IGrupoUsuarioService, GrupoUsuario, GrupoUsuarioSummary, Guid>();
            new Domain.Notifications.EntityNotifier<ILocalizacaoService, Localizacao, LocalizacaoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IPassageiroService, Passageiro, PassageiroSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IPontoTaxiService, PontoTaxi, PontoTaxiSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IRotaService, Rota, RotaSummary, Guid>();
            //new Domain.Notifications.EntityNotifier<ISolicitacaoCorridaService, SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>(); // COMPAT
            new Domain.Notifications.EntityNotifier<ITarifaService, Tarifa, TarifaSummary, Guid>();
            new Domain.Notifications.EntityNotifier<ITaxistaService, Taxista, TaxistaSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IUsuarioGrupoUsuarioService, UsuarioGrupoUsuario, UsuarioGrupoUsuarioSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IVeiculoService, Veiculo, VeiculoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IVeiculoTaxistaService, VeiculoTaxista, VeiculoTaxistaSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IContatoService, Contato, ContatoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFaturamentoService, Faturamento, FaturamentoSummary, Guid>();
            new Domain.Notifications.EntityNotifier<IFaturamentoTaxistaService, FaturamentoTaxista, FaturamentoTaxistaSummary, Guid>();

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
            services.AddTransient<IFaturamentoRepository, FaturamentoRepository>();
            services.AddTransient<IFaturamentoTaxistaRepository, FaturamentoTaxistaRepository>();
            services.AddTransient<IFaixaAtivacaoRepository, FaixaAtivacaoRepository>();
            services.AddTransient<IUFRepository, UFRepository>();
            services.AddTransient<ICidadeRepository, CidadeRepository>();
            services.AddTransient<IHabilitacaoRepository, HabilitacaoRepository>();
            services.AddTransient<IRegistroVeiculoRepository, RegistroVeiculoRepository>();

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

        public static void ConfigureSignalR(IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<HubNotificacoes>("/notifications");
                routes.MapHub<HubNotificaoesAdmin>("/notifications/admin");

                // COMPAT
                routes.MapHub<HubLocalizacaoTaxista>("/notifications/localizacao_taxista");
                routes.MapHub<HubLocalizacaoPassageiro>("/notifications/localizacao_passageiro");
                routes.MapHub<HubMensagens>("/notifications/mensagens");
                routes.MapHub<Domain.Notifications.Compat.EntityNotifier<ICorridaService, Corrida, CorridaSummary, Guid>>("/notifications/corrida");
                routes.MapHub<Domain.Notifications.Compat.EntityNotifier<ISolicitacaoCorridaService, SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>>("/notifications/solicitacao_corrida");
            });
        }
    }
}
