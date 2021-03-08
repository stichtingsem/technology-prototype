using Abp;
using Abp.Dependency;
using Abp.Webhooks;
using Domain.EventTypes;
using Domain.Tenants;
using Domain.Webhooks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestService.Authorization;
using RestService.Catalog;
using RestService.Filters;
using RestService.Implementations;
using RestService.Tenants;
using SqlRepositories.WebhookSubscriptions;
using System;
using System.IO;


namespace RestService
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnv = env;
            Configuration = configuration;
        }

        private readonly IWebHostEnvironment _hostingEnv;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Authentication:Authority"];
                    options.Audience = Configuration["Authentication:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = Convert.ToBoolean(Configuration["Authentication:ValidateIssuer"]),
                        ValidateIssuerSigningKey = Convert.ToBoolean(Configuration["Authentication:ValidateIssuerSigningKey"]),
                        ValidateAudience = Convert.ToBoolean(Configuration["Authentication:ValidateAudience"]),
                    };
                });

            services.AddTransient<WebhooksSqlConnection>();
            services.AddTransient<WebhooksSqlConnectionString>((serviceProvider) => Configuration["ConnectionStrings:Webhooks"]);
            services.AddTransient<IEventTypesRepository, FixedEventTypesRepository>();
            services.AddTransient<IWebhooksRepository, AbpWebhookRepository>();
            services.AddTransient<ITenantRepository, TenantRepository>();
            services.AddTransient<ITenant, TenantFromHttpContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<TenantIdClaimType>();


            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                })
                .AddXmlSerializerFormatters();

            services
               .AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("1.0.0", new OpenApiInfo
                   {
                       Version = "1.0.0",
                       Title = "SEM API",
                       Description = "SEM API - Reliable and near real time integration",
                       Contact = new OpenApiContact()
                       {
                           Name = "Stichting Educatieve Marktpartijen",
                           Url = new Uri("https://stichtingsem.stoplight.io/docs/sem-technology-prototype"),
                           //Email = "clifton@infinitaslearning.com"
                       }//,
                       //TermsOfService = new Uri("")
                   });
                   c.CustomSchemaIds(type => type.FullName);
                   c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                // Sets the basePath property in the Swagger document generated
                c.DocumentFilter<BasePathFilter>("/api");

                // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                c.OperationFilter<GeneratePathParamsValidationFilter>();
               });

            var bootStrapper = AbpBootstrapper.Create<MyAppModule>();
            bootStrapper.Initialize();

            IocManager.Instance.Register<IWebhookEventStore, WebhookEventStore>();
            IocManager.Instance.Register<IWebhookSendAttemptStore, WebhookSendAttemptStore>();
            IocManager.Instance.Register<IWebhookSubscriptionsStore, WebhookSubscriptionsStore>();


            Console.WriteLine("create definition");

            var definitionMngr = IocManager.Instance.Resolve<IWebhookDefinitionManager>();
            definitionMngr.Add(new WebhookDefinition("catalog.added"));
            definitionMngr.Add(new WebhookDefinition("catalog.updated"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Generic Event Stream API");

                //TODO: Or alternatively use the original Swagger contract that's included in the static files
                // c.SwaggerEndpoint("/swagger-original.json", "Generic Event Stream API Original");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }
        }
    }
}
