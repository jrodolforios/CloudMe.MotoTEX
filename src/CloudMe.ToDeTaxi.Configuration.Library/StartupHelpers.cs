﻿using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CloudMe.ToDeTaxi.Configuration.Library.Constants;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using System;
using CloudMe.ToDeTaxi.Domain.Services;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Notifications;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using CloudMe.ToDeTaxi.Domain.Model.Veiculo;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using CloudMe.ToDeTaxi.Domain.Model.Foto;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using Microsoft.AspNetCore.SignalR;
using CloudMe.ToDeTaxi.Configuration.Library.SignalR;
using EntityFrameworkCore.Triggers;
using EntityFrameworkCore.Triggers.AspNetCore;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts.Background;
using CloudMe.ToDeTaxi.Domain.Services.Background;
using CloudMe.ToDeTaxi.Domain.Notifications.Hubs;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstracts;

namespace CloudMe.ToDeTaxi.Configuration.Library.Helpers
{
    public static class StartupHelpers
    {
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

        public static IServiceCollection AddToDeTaxiServices(this IServiceCollection services)
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(p => p.GetService<IHttpContextAccessor>()?.HttpContext);

            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddTriggers();

            services.AddSingleton<IPoolLocalizacaoTaxista, PoolLocalizacaoTaxista>();
            services.AddSingleton<IPoolLocalizacaoPassageiro, PoolLocalizacaoPassageiro>();

            return services;
        }
        public static IServiceCollection AddToDeTaxiRepositories(this IServiceCollection services)
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

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static async void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CloudMeToDeTaxiContext>())
                {
                    context.Database.Migrate();

                    // DEBUB - REMOVER APÓS PRIMEIRA MIGRAÇÃO
                    const string latitude = "-17.8588";
                    const string longitude = "-41.509";
                    await context.Taxistas.ForEachAsync(taxista =>
                    {
                        if (!taxista.IdLocalizacaoAtual.HasValue)
                        {
                            Localizacao localizacao = new Localizacao();
                            localizacao.IdUsuario = taxista.IdUsuario;
                            localizacao.Latitude = latitude;
                            localizacao.Longitude = longitude;
                            context.Entry(localizacao).State = EntityState.Added;

                            taxista.IdLocalizacaoAtual = localizacao.Id;
                            context.Entry(taxista).State = EntityState.Modified;
                        }
                    });

                    await context.Passageiros.ForEachAsync(passageiro =>
                    {
                        if (!passageiro.IdLocalizacaoAtual.HasValue)
                        {
                            Localizacao localizacao = new Localizacao();
                            localizacao.IdUsuario = passageiro.IdUsuario;
                            localizacao.Latitude = latitude;
                            localizacao.Longitude = longitude;
                            context.Entry(localizacao).State = EntityState.Added;

                            passageiro.IdLocalizacaoAtual = localizacao.Id;
                            context.Entry(passageiro).State = EntityState.Modified;
                        }
                    });

                    context.SaveChanges();
                }
            }
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization();
            /*services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
                    policy => policy.RequireRole(AuthorizationConsts.AdministrationRole));
            });*/
        }

        public static void ConfigureSignalR<TContext>(IApplicationBuilder app) where TContext: DbContext
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<EntityNotifier<TContext, ICorridaService, Corrida, CorridaSummary, Guid>>("/notifications/corrida");
                routes.MapHub<EntityNotifier<TContext, ICorVeiculoService, CorVeiculo, CorVeiculoSummary, Guid>>("/notifications/cor_veiculo");
                routes.MapHub<EntityNotifier<TContext, IEnderecoService, Endereco, EnderecoSummary, Guid>>("/notifications/endereco");
                routes.MapHub<EntityNotifier<TContext, IFaixaDescontoService, FaixaDesconto, FaixaDescontoSummary, Guid>>("/notifications/faixa_desconto");
                routes.MapHub<EntityNotifier<TContext, IFaixaDescontoTaxistaService, FaixaDescontoTaxista, FaixaDescontoTaxistaSummary, Guid>>("/notifications/faixa_desconto_taxista");
                routes.MapHub<EntityNotifier<TContext, IFavoritoService, Favorito, FavoritoSummary, Guid>>("/notifications/favorito");
                routes.MapHub<EntityNotifier<TContext, IFormaPagamentoService, FormaPagamento, FormaPagamentoSummary, Guid>>("/notifications/forma_pagamento");
                routes.MapHub<EntityNotifier<TContext, IFormaPagamentoTaxistaService, FormaPagamentoTaxista, FormaPagamentoTaxistaSummary, Guid>>("/notifications/forma_pagamento_taxista");
                routes.MapHub<EntityNotifier<TContext, IFotoService, Foto, FotoSummary, Guid>>("/notifications/foto");
                routes.MapHub<UsuarioNotifier<TContext>>("/notifications/usuario");
                routes.MapHub<EntityNotifier<TContext, IGrupoUsuarioService, GrupoUsuario, GrupoUsuarioSummary, Guid>>("/notifications/grupo_usuario");
                routes.MapHub<EntityNotifier<TContext, ILocalizacaoService, Localizacao, LocalizacaoSummary, Guid>>("/notifications/localizacao");
                routes.MapHub<EntityNotifier<TContext, IPassageiroService, Passageiro, PassageiroSummary, Guid>>("/notifications/passageiro");
                routes.MapHub<EntityNotifier<TContext, IPontoTaxiService, PontoTaxi, PontoTaxiSummary, Guid>>("/notifications/ponto_taxi");
                routes.MapHub<EntityNotifier<TContext, IRotaService, Rota, RotaSummary, Guid>>("/notifications/rota");
                routes.MapHub<EntityNotifier<TContext, ISolicitacaoCorridaService, SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>>("/notifications/solicitacao_corrida");
                routes.MapHub<EntityNotifier<TContext, ITarifaService, Tarifa, TarifaSummary, Guid>>("/notifications/tarifa");
                routes.MapHub<EntityNotifier<TContext, ITaxistaService, Taxista, TaxistaSummary, Guid>>("/notifications/taxista");
                routes.MapHub<EntityNotifier<TContext, IUsuarioGrupoUsuarioService, UsuarioGrupoUsuario, UsuarioGrupoUsuarioSummary, Guid>>("/notifications/usuario_grupo_usuario");
                routes.MapHub<EntityNotifier<TContext, IVeiculoService, Veiculo, VeiculoSummary, Guid>>("/notifications/veiculo");
                routes.MapHub<EntityNotifier<TContext, IVeiculoTaxistaService, VeiculoTaxista, VeiculoTaxistaSummary, Guid>>("/notifications/veiculo_taxista");

                routes.MapHub<HubLocalizacaoTaxista>("/notifications/localizacao_taxista");
                routes.MapHub<HubLocalizacaoPassageiro>("/notifications/localizacao_passageiro");
            });
        }

        public static void BuildDBTriggers(IApplicationBuilder app)
        {
            app.UseTriggers(builder =>
            {

            });
        }

        /*public static void AddTriggers(IApplicationBuilder app)
        {
            app.UseTriggers(builder =>
            {
                builder.Triggers<MercuryActor, MessageBrokerContext>.Inserted += entry =>
                {
                    MercuryActor actor = entry.Entity;
                    Logger.LogInformation("{0} Inserted! w/ Context", actor.Id);
                };
                Logger.LogInformation("Triggers declared");
            });
        }*/
    }
}
