using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System.Collections.Generic;
using static SqlRepositoriesTests.SubscriptionsRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public class AddTests
    {
        SubscriptionsSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SubscriptionsSqlRepository();
        }

        [Test]
        public void AddSubscription()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var subscription = new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(subscription);

            Assert.AreEqual(subscription, sut.Get(subscriptionId, schoolId));
        }
    }
}
