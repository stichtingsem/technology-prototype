using Domain.Events;
using Domain.Tenants;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public abstract class WebhooksSqlRepositorySetup
    {
        protected WebhooksSqlRepository sut;

        private readonly RepositoryFactory factory = new RepositoryFactory();
        private readonly IEnumerable<EventId> eventIds;

        public WebhooksSqlRepositorySetup()
        {
            var eventsRepo = factory.EventsSqlRepository;
            eventIds = eventsRepo.GetAll().Select(ev => ev.Id);
        }

        [SetUp]
        public void SetUp()
        {
            sut = factory.WebhooksSqlRepository;
        }

        public WebhookId RandomWebhookId() => Guid.NewGuid();

        public TenantId RandomTenantId() => Guid.NewGuid();

        public IEnumerable<EventId> ListOfDistinctEventIds() => eventIds.Take(2);

    }
}
