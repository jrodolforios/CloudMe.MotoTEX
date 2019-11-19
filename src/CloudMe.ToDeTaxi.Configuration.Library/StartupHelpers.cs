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
using Microsoft.Extensions.Hosting;

namespace CloudMe.ToDeTaxi.Configuration.Library.Helpers
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
            services.AddTransient<IContratoService, ContratoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(p => p.GetService<IHttpContextAccessor>()?.HttpContext);

            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddTriggers();

            services.AddSingleton<PoolLocalizacaoTaxista>();
            services.AddSingleton<IHostedService, PoolLocalizacaoTaxista>(serviceProvider =>serviceProvider.GetService<PoolLocalizacaoTaxista>());

            services.AddSingleton<PoolLocalizacaoPassageiro>();
            services.AddSingleton<IHostedService, PoolLocalizacaoPassageiro>(serviceProvider => serviceProvider.GetService<PoolLocalizacaoPassageiro>());

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
            services.AddTransient<IContratoRepository, ContratoRepository>();

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

                routes.MapHub<HubLocalizacaoTaxista>("/notifications/localizacao_taxista");
                routes.MapHub<HubLocalizacaoPassageiro>("/notifications/localizacao_passageiro");
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
