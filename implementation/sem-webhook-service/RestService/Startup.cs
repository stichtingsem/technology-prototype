using Domain.Tenants;
using Domain.EventTypes;
using Domain.Webhooks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RestService.Authorization;
using System;
using RestService.Tenants;
using SqlRepositories.EventTypes;
using SqlRepositories.Webhooks;

namespace RestService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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

            services.AddTransient<EventTypesSqlConnectionString>((serviceProvider) => Configuration["ConnectionStrings:EventTypes"]);
            services.AddTransient<WebhooksSqlConnectionString>((serviceProvider) => Configuration["ConnectionStrings:Webhooks"]);
            services.AddTransient<IEventTypesRepository, EventTypesSqlRepository>();
            services.AddTransient<IWebhooksRepository, WebhooksSqlRepository>();
            services.AddTransient<ITenant, TenantFromHttpContext>();
            services.AddTransient<TenantIdClaimType>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
