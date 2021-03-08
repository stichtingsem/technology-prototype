using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;

namespace RestService.Authorization
{
    public sealed class AuthorizeFromConfigAttribute : AuthorizeAttribute
    {
        public AuthorizeFromConfigAttribute()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .Build();

            Roles = config["Authorization:Roles"];
        }
    }
}
