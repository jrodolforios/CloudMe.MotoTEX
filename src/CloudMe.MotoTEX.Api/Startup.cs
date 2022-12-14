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
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Configuration.Library.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Extensions;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using System.Net;
using EntityFrameworkCore.Triggers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Serilog;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

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

namespace CloudMe.MotoTEX.Api
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
            CORSConfig corsConfig = new CORSConfig();
            Configuration.GetSection("CORSConfig").Bind(corsConfig);

            services.AddIdentity<Usuario, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
            })
            //.AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<CloudMeMotoTEXContext>()
            .AddDefaultTokenProviders();

            services.AddAdminAspNetIdentityServices<CloudMeMotoTEXContext, UserDto<Guid>, Guid, RoleDto<Guid>, Guid, Guid, Guid,
                                Usuario,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
                                 IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>();

            var authorityBaseUrl = Configuration["Identity:Authority"];
            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authorityBaseUrl;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "mototexapi";

                    options.TokenRetriever = request =>
                    {
                        var accessToken = request.Query["access_token"];

                        // If the request is for our hubs...
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (request.Path.StartsWithSegments("/notifications")))
                        {
                            // Read the token out of the query string
                            return TokenRetrieval.FromQueryString().Invoke(request);
                        }
                        else
                        {
                            // Read the token out of the request headers
                            return TokenRetrieval.FromAuthorizationHeader().Invoke(request);
                        }
                    };
                });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "CloudMe MotoTEX API", Version = "v1" });
                
                x.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "implicit",
                    AuthorizationUrl = authorityBaseUrl + "/connect/authorize",
                    Scopes = new Dictionary<string, string>
                    {
                        { "mototexapi", "mototexapi" } 
                    }
                });

                x.OperationFilter<AuthorizeCheckOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddDbContexts<CloudMeMotoTEXContext>(Configuration);
            services.AddMotoTEXRepositories();
            services.AddMotoTEXServices();

            services.AddAuthorizationPolicies();

            /*services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.WithOrigins("http://localhost:8080", "https://MotoTEX.cloudme.com.br/", "http://MotoTEX.cloudme.com.br/");
                    options.WithHeaders("Authorization", "content-type");
                    options.AllowAnyMethod();
                });
            });*/

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
            });

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
                    if (corsConfig.headers.allowAny)
                    {
                        options.AllowAnyHeader();
                    }
                    else
                    {
                        options.WithHeaders(corsConfig.headers.allowed);
                    }

                    if (corsConfig.methods.allowAny)
                    {
                        options.AllowAnyMethod();
                    }
                    else
                    {
                        options.WithMethods(corsConfig.methods.allowed);
                    }

                    if (corsConfig.origins.allowAny)
                    {
                        options.AllowAnyOrigin();
                    }
                    else
                    {
                        options.WithOrigins(corsConfig.origins.allowed);
                    }

                    if (corsConfig.credentials)
                    {
                        options.AllowCredentials();
                    }
                    else
                    {
                        options.DisallowCredentials();
                    }
                });
            });

            /*services.TryAdd(ServiceDescriptor.Transient<ICorsService, WildCardCorsService>());
            services.AddCors
            (
                options =>
                {
                    options.AddPolicy
                    (
                        "AllowOrigin",
                        builder =>
                        {
                            if (corsConfig.headers.allowAny)
                            {
                                builder.AllowAnyHeader();
                            }
                            else
                            {
                                builder.WithHeaders(corsConfig.headers.allowed);
                            }

                            if (corsConfig.methods.allowAny)
                            {
                                builder.AllowAnyMethod();
                            }
                            else
                            {
                                builder.WithMethods(corsConfig.methods.allowed);
                            }

                            if (corsConfig.origins.allowAny)
                            {
                                builder.AllowAnyOrigin();
                            }
                            else
                            {
                                builder.WithOrigins(corsConfig.origins.allowed);
                            }

                            if (corsConfig.credentials)
                            {
                                builder.AllowCredentials();
                            }
                            else
                            {
                                builder.DisallowCredentials();
                            }
                        }
                    );
                }
            );*/

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddTriggers();//
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            Log.Logger = new LoggerConfiguration()
                            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/Logs/CLOUDME_MotoTEX_API_.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null)
                            .CreateLogger();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudMe MotoTEX - V1");
                c.RoutePrefix = "swagger";
                c.OAuthClientId("MotoTEXAPI_swagger");
                c.OAuthAppName("MotoTEX API - Swagger");
            });


            StartupHelpers.ConfigureSignalR(app);

            app.UseMvc();
        }

    }
}
