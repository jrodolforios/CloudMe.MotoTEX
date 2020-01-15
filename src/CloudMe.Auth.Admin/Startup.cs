using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Extensions;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Extensions;
using CloudMe.MotoTEX.Domain.Services;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.Auth.Admin.Configuration.Interfaces;
using CloudMe.Auth.Admin.Helpers;
using CloudMe.Auth.Admin.Model;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace CloudMe.Auth.Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();

            HostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureRootConfiguration(Configuration);
            var rootConfiguration = services.BuildServiceProvider().GetService<IRootConfiguration>();

            services.AddDbContexts<CloudMeMotoTEXContext>(HostingEnvironment, Configuration);
            services.AddAuthenticationServices<CloudMeMotoTEXContext, CloudMe.MotoTEX.Infraestructure.Entries.Usuario,IdentityRole<Guid>>(HostingEnvironment, rootConfiguration.AdminConfiguration);
            services.AddMvcExceptionFilters();

            services.AddAdminServices<CloudMeMotoTEXContext>();

            services.AddAdminAspNetIdentityServices<CloudMeMotoTEXContext, CloudMeUserDto, Guid, RoleDto<Guid>, Guid, Guid, Guid,
                                CloudMe.MotoTEX.Infraestructure.Entries.Usuario,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
                                 IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>();

            services.AddMvcLocalization();
            services.AddAuthorizationPolicies();

            services.AddTransient<IUsuarioService, UsuarioService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(p => p.GetService<IHttpContextAccessor>()?.HttpContext);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.AddLogging(loggerFactory, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSecurityHeaders();
            app.UseStaticFiles();
            app.ConfigureAuthentificationServices(env);
            app.ConfigureLocalization();
            //app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}