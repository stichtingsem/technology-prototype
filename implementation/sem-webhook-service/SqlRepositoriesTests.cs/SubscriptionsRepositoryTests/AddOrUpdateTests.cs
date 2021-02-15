using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System.Linq;
using static SqlRepositoriesTests.SubscriptionsRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public class AddOrUpdateTests
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
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            var subscription = new Subscription(schoolId, eventId, "postbackUrl", "secret");

            sut.AddOrUpdate(subscription);

            Assert.AreEqual(subscription, sut.Get(schoolId, eventId));
        }

        [Test]
        public void UpdateSubscription()
        {
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            var subscription1 = new Subscription(schoolId, eventId, "postbackUrl", "secret");
            var subscription2 = new Subscription(schoolId, eventId, "updatedUrl", "updatedSecret");

            sut.AddOrUpdate(subscription1);
            sut.AddOrUpdate(subscription2);

            Assert.AreEqual(1, sut.GetAll(schoolId).Count());
        }
    }
}
