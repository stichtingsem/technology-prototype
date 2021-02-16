using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using System;
using System.Collections.Generic;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public static class IdHelpers
    {
        public static SubscriptionId RandomSubscriptionId() => Guid.NewGuid();
        public static SchoolId RandomSchoolId() => Guid.NewGuid();
        public static EventId RandomEventId() => Guid.NewGuid();
        public static IEnumerable<EventId> ListOfRandomEventIds() => new List<EventId> { RandomEventId(), RandomEventId() };
    }
}
