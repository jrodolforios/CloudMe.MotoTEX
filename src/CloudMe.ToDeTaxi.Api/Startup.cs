using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Configuration.Library.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Extensions;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using System.Net;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(Operation operation, OperationFilterContext context)
    {
        var hasAuthorize = context.ControllerActionDescriptor.GetControllerAndActionAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (hasAuthorize)
        {
            operation.Responses.Add("401", new Response { Description = "Unauthorized" });
            operation.Responses.Add("403", new Response { Description = "Forbidden" });

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>> {{"oauth2", new[] {"demo_api"}}}
            };
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

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<Usuario, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<CloudMeToDeTaxiContext>()
                .AddDefaultTokenProviders();

            services.AddAdminAspNetIdentityServices<CloudMeToDeTaxiContext, UserDto<Guid>, Guid, RoleDto<Guid>, Guid, Guid, Guid,
                                CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
                                 IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>();

            var authorityBaseUrl = Configuration["Identity:Authority"];
            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(c =>
                {
                    c.Authority = authorityBaseUrl;
                    c.RequireHttpsMetadata = false;
                    c.ApiName = "todetaxiapi";
                });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "CloudMe ToDeTaxi API", Version = "v1" });
                
                x.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "implicit",
                    //AuthorizationUrl = "https://auth.todetaxi.com.br/connect/authorize",
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        { "todetaxiapi", "TOdeTaxiAPI" } 
                    }
                });

                x.OperationFilter<AuthorizeCheckOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddDbContexts<CloudMeToDeTaxiContext>(Configuration);
            services.AddToDeTaxiServices()
                    .AddToDeTaxiRepositories();

            services.AddAuthorizationPolicies();

            /*services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.WithOrigins("http://localhost:8080", "https://todetaxi.cloudme.com.br/", "http://todetaxi.cloudme.com.br/");
                    options.WithHeaders("Authorization", "content-type");
                    options.AllowAnyMethod();
                });
            });*/

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    });

            // Get host name
            string strHostName = Dns.GetHostName();

            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            List<string> urls = new List<string>()
            {
                "http://localhost", "http://127.0.0.1", "http://localhost:4200"
            };

            // Enumerate IP addresses
            foreach (IPAddress ipaddress in iphostentry.AddressList.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
                Console.WriteLine(ipaddress.ToString());
                urls.Add($"https://{ipaddress.ToString()}");
                urls.Add($"http://{ipaddress.ToString()}");
            }

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            StartupHelpers.UpdateDatabase(app);

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
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseCors("AllowOrigin");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudMe ToDeTaxi - V1");
                c.RoutePrefix = string.Empty;
                c.OAuthClientId("ToDeTaxiAPI_swagger");
                c.OAuthAppName("TOdeTaxi API - Swagger");
            });

            app.UseMvc();
        }

    }
}
