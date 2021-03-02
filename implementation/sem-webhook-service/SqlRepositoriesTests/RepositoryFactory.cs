using Microsoft.Extensions.Configuration;
using SqlRepositories.EventTypes;
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

        public EventTypesSqlRepository EventsTypesSqlRepository => new EventTypesSqlRepository(config["ConnectionStrings:EventTypes"]);

        public WebhooksSqlRepository WebhooksSqlRepository => new WebhooksSqlRepository(config["ConnectionStrings:Webhooks"]);

        public WebhookSubscriptionsSqlStore WebhookSubscriptionsSqlStore => new WebhookSubscriptionsSqlStore(config["ConnectionStrings:Webhooks"]);
    }
}
