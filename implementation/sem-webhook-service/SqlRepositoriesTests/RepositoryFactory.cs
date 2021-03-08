using Microsoft.Extensions.Configuration;
using SqlRepositories.EventTypes;
using SqlRepositories.WebhookEvents;
using SqlRepositories.WebhookSendAttempts;
using SqlRepositories.WebhookSubscriptions;
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

        public WebhookSubscriptionsSqlStore WebhookSubscriptionsSqlStore => new WebhookSubscriptionsSqlStore(new WebhooksSqlConnection(config["ConnectionStrings:Webhooks"]));

        public WebhookEventsSqlStore WebhookEventsSqlStore => new WebhookEventsSqlStore(new WebhooksSqlConnection(config["ConnectionStrings:Webhooks"]));

        public WebhookSendAttemptSqlStore WebhookSendAttemptSqlStore => new WebhookSendAttemptSqlStore(new WebhooksSqlConnection(config["ConnectionStrings:Webhooks"]));
    }
}
