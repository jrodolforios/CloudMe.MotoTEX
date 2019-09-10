using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CloudMe.ToDeTaxi.Configuration.Library.Helpers;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Linq;

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

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "CloudMe ToDeTaxi API", Version = "v1" });

                x.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

                //x.OperationFilter<FileUploadOperation>(); //Register File Upload Operation Filter
            });

            services.ConfigureSwaggerGen(options => {
                options.OperationFilter<FileUploadOperation>(); //Register File Upload Operation Filter
            });

            services.AddAuthorization();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var isDevelopment = services.BuildServiceProvider().GetService<IHostingEnvironment>().IsDevelopment();
                    options.Authority = isDevelopment ? "https://localhost:5001" : "https://authToDeTaxi.CloudMeweb.com.br";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "ToDeTaxiapi";
                });

            services.AddDbContexts<CloudMeToDeTaxiContext>(Configuration);
            services.AddToDeTaxiServices()
                    .AddToDeTaxiRepositories();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.WithOrigins("http://localhost:8080", "https://ToDeTaxi.CloudMeweb.com.br/", "http://ToDeTaxi.CloudMeweb.com.br/");
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
            //app.ApiAllowOrigin();
            app.UseSwagger();
            app.UseMvc();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudMe ToDeTaxi - V1");
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
    }
}
