using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CloudMe.ToDeTaxi.Configuration.Library.Helpers;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace CloudMe.ToDeTaxi.IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }
        public Startup(IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContexts<CloudMeToDeTaxiContext>(Configuration);
            services.AddAuthenticationServices<CloudMeToDeTaxiContext, CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>>(Environment, Configuration);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            StartupHelpers.UpdateDatabase(app);

            StartupHelpers.InitializeTokenServerConfigurationDatabase(app);

            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}