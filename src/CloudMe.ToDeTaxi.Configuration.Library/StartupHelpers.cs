using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CloudMe.ToDeTaxi.Domain.Services;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Configuration.Library.Constants;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Repositories;
using System;
using System.Reflection;

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
            services.AddDbContext<TContext>(options => options.UseMySql(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));
            return services;
        }

        public static IServiceCollection AddToDeTaxiServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICallService, CallService>();

            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(p => p.GetService<IHttpContextAccessor>()?.HttpContext);


            return services;
        }
        public static IServiceCollection AddToDeTaxiRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICallRepository, CallRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
