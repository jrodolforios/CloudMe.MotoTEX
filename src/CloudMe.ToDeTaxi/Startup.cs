using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CloudMe.ToDeTaxi.Configuration.Library.Helpers;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using CloudMe.ToDeTaxi.Helpers.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CloudMe.ToDeTaxi.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace CloudMe.ToDeTaxi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public ILogger Logger { get; set; }

        public Startup(IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            Environment = environment;
            //Logger = loggerFactory.CreateLogger<Startup>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var fullPath = Assembly.GetEntryAssembly().Location;
            string theDirectory = Path.GetDirectoryName(fullPath);

            var theDirectoryPath = Path.Combine(theDirectory, "dataprotection");

            if (!Directory.Exists(theDirectoryPath))
            {
                Directory.CreateDirectory(theDirectoryPath);
            }

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(theDirectoryPath));    
            
            services.AddDbContexts<CloudMeToDeTaxiContext>(Configuration);
            services.AddEmailSenders(Configuration);
            services.AddAuthenticationServices<CloudMeToDeTaxiContext, CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario, IdentityRole<Guid>>(Environment, Configuration, Logger);
            services.AddMvcLocalization();
            services.Configure<IISServerOptions>(iisoptions =>
            {
                iisoptions.AutomaticAuthentication = true;
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CloudMeToDeTaxiContext CloudMeToDeTaxiContext)
        {
            //app.AddLogging(loggerFactory, Configuration);

            Log.Logger = new LoggerConfiguration()
                            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/Logs/CLOUDME_TODETAXI_AUTH_.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null)
                            .CreateLogger();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseSecurityHeaders();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcLocalizationServices();
            app.UseMvcWithDefaultRoute();
        }
    }
}
