using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CloudMe.ToDeTaxi.Api.Configuration.Helpers;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

public class FileUploadOperation : IOperationFilter
{
    public void Apply(Operation operation, OperationFilterContext context)
    {
        if (operation.Parameters.FirstOrDefault(x => x.Name.ToLower() == "arquivo") != null)
        {
            operation.Parameters.Clear();
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "arquivo",
                In = "formData",
                Description = "Upload File",
                Required = true,
                Type = "file"
            });
            operation.Consumes.Add("multipart/form-data");
        }
    }
}

public class AuthorizeCheckOperationFilter : IOperationFilter 
{
    public void Apply(Operation operation, OperationFilterContext context)
    {
        /*var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Any();

        if (hasAuthorize)
        {
            operation.Responses.Add("401", new Response { Description = "Unauthorized" });
            operation.Responses.Add("403", new Response { Description = "Forbidden" });

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>> {{"oauth2", new[] {"todetaxiapi"}}}
            };
        }*/
        // Policy names map to scopes
        var controllerScopes = context.ApiDescription.ControllerAttributes()
            .OfType<AuthorizeAttribute>()
            .Select(attr => attr.Policy);

        var actionScopes = context.ApiDescription.ActionAttributes()
            .OfType<AuthorizeAttribute>()
            .Select(attr => attr.Policy);

        var requiredScopes = controllerScopes.Union(actionScopes).Distinct();

        if (requiredScopes.Any())
        {
            operation.Responses.Add("401", new Response { Description = "Unauthorized" });
            operation.Responses.Add("403", new Response { Description = "Forbidden" });

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            operation.Security.Add(new Dictionary<string, IEnumerable<string>>
            {
                { "oauth2", requiredScopes }
            });
        }
    }
}

namespace CloudMe.ToDeTaxi.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public ILoggerFactory _loggerFactory { get; set; }

        public Startup(IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
            _loggerFactory = loggerFactory;

            /*var serilog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(@"identityserver4_log.txt");

            if (environment.IsDevelopment())
            {
                serilog.WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}{Exception}{NewLine}");
            }

            loggerFactory
            .WithFilter(new FilterLoggerSettings
            {
                { "IdentityServer", LogLevel.Debug },
                { "Microsoft", LogLevel.Information },
                { "System", LogLevel.Error },
            })
            .AddSerilog(serilog.CreateLogger());*/
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Identity:Authority"];
                    options.ApiName = "todetaxiapi";
                    options.RequireHttpsMetadata = false; // dev only!
                });

            services.AddAuthorization();
            /*services.AddAuthorization(c =>
            {
                c.AddPolicy("todetaxiapi", p => p.RequireClaim("scope", "todetaxiapi"));
                //c.AddPolicy("writeAccess", p => p.RequireClaim("scope", "writeAccess"));
            });*/

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "CloudMe ToDeTaxi API", Version = "v1" });

                x.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "/connect/authorize",
                    Scopes = new Dictionary<string, string> { 
                        { "todetaxiapi", "TOdeTaxiAPI - Seguro" } 
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.OperationFilter<FileUploadOperation>(); //Register File Upload Operation Filter
                x.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddDbContexts<CloudMeToDeTaxiContext>(Configuration);
            services.AddAuthenticationServices<CloudMeToDeTaxiContext, CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>>(Environment, Configuration);
            services.AddToDeTaxiServices()
                    .AddToDeTaxiRepositories();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.WithOrigins("http://localhost:44315", "https://todetaxi.cloudme.com.br/", "http://todetaxi.cloudme.com.br/");
                    options.WithHeaders("Authorization", "content-type");
                    options.AllowAnyMethod();
                });
            });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            UpdateDatabase(app);

            InitializeTokenServerConfigurationDatabase(app, Configuration);

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // ===== Use Authentication ======
            app.UseAuthentication();
            app.UseIdentityServer();
            //app.UseHttpsRedirection();

            //app.ApiAllowOrigin();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudMe ToDeTaxi - V1");
                c.OAuthClientId("ToDeTaxiAPI_swagger");
                c.OAuthAppName("TOdeTaxi API - Swagger");
            });
            app.UseCors("AllowOrigin");

            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                      .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CloudMeToDeTaxiContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private static void InitializeTokenServerConfigurationDatabase(IApplicationBuilder app, IConfiguration Configuration)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                //context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients(Configuration))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
