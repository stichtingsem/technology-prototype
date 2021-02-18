using Microsoft.Extensions.Configuration;
using SqlRepositories.Events;
using SqlRepositories.Webhooks;
using System;

namespace SqlRepositoriesTests
{
    public class RepositoryFactory
    {
        private readonly IConfigurationRoot config;

        public RepositoryFactory()
        {
            config = new ConfigurationBuilder().
                AddJsonFile("appsettings.json").
                AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true).
                Build();
        }

        public EventsSqlRepository EventsSqlRepository => new EventsSqlRepository(config["ConnectionStrings:Events"]);

        public WebhooksSqlRepository WebhooksSqlRepository => new WebhooksSqlRepository(config["ConnectionStrings:Webhooks"]);
    }
}
