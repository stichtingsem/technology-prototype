using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using System;
using System.Collections.Generic;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public static class IdHelpers
    {
        public static WebhookId RandomWebhookId() => Guid.NewGuid();
        public static SchoolId RandomSchoolId() => Guid.NewGuid();
        public static EventId RandomEventId() => Guid.NewGuid();
        public static IEnumerable<EventId> ListOfRandomEventIds() => new List<EventId> { RandomEventId(), RandomEventId() };
    }
}
