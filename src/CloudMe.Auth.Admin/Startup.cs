using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Extensions;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Extensions;
using CloudMe.Auth.Admin.Configuration.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Entities.Identity;
using CloudMe.Auth.Admin.Helpers;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using CloudMe.ToDeTaxi.Domain.Services;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Repositories;
using CloudMe.Auth.Admin.Model;

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

            services.AddDbContexts<CloudMeToDeTaxiContext>(HostingEnvironment, Configuration);
            services.AddAuthenticationServices<CloudMeToDeTaxiContext, CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>>(HostingEnvironment, rootConfiguration.AdminConfiguration);
            services.AddMvcExceptionFilters();

            services.AddAdminServices<CloudMeToDeTaxiContext>();

            services.AddAdminAspNetIdentityServices<CloudMeToDeTaxiContext, CloudMeUserDto, Guid, RoleDto<Guid>, Guid, Guid, Guid,
                                CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
                                 IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>();

            services.AddMvcLocalization();
            services.AddAuthorizationPolicies();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}