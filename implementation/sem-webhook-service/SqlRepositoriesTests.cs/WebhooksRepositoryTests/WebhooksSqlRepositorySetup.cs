using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories.Events;
using SqlRepositories.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public abstract class WebhooksSqlRepositorySetup
    {
        protected WebhooksSqlRepository sut;

        private readonly IEventsRepository eventsRepo = new EventsSqlRepository();
        private readonly IEnumerable<EventId> eventIds;

        public WebhooksSqlRepositorySetup()
        {
            eventIds = eventsRepo.GetAll().Select(e => e.Convert((eventId, eventName) => eventId));
        }

        [SetUp]
        public void SetUp()
        {
            sut = new WebhooksSqlRepository();
        }

        public WebhookId RandomWebhookId() => Guid.NewGuid();

        public SchoolId RandomSchoolId() => Guid.NewGuid();

        public IEnumerable<EventId> ListOfDistinctEventIds() => eventIds.Take(2);

    }
}
