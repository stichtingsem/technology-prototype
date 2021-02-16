using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System.Collections.Generic;
using System.Linq;
using static SqlRepositoriesTests.SubscriptionsRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public class UpdateTests
    {
        SubscriptionsSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SubscriptionsSqlRepository();
        }



        [Test]
        public void UpdateSubscription()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var subscription1 = new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret");
            var subscription2 = new Subscription(subscriptionId, schoolId, eventIds, "updatedUrl", "updatedSecret");

            sut.Add(subscription1);

            sut.Update(subscription2, schoolId);

            Assert.AreEqual(1, sut.GetAll(schoolId).Count());
        }
    }
}
