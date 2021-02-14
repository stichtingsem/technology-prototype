using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public class GetTests
    {
        SubscriptionsSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SubscriptionsSqlRepository();
        }

        [Test]
        public void SubscriptionDoesNotExist()
        {
            SchoolId schoolId = "DoesNotExist";
            EventId eventId = Guid.NewGuid();

            var result = sut.Get(schoolId, eventId);

            Assert.IsNull(result);
        }

        [Test]
        public void SchoolIdDoesNotExist()
        {
            SchoolId schoolId = "SchoolId";
            EventId eventId = Guid.NewGuid();

            sut.AddOrUpdate(new Subscription(schoolId, eventId,  "postbackUrl", "secret"));

            var result = sut.Get("DoesNotExist", eventId);

            Assert.IsNull(result);
        }

        [Test]
        public void EventIdDoesNotExist()
        {
            SchoolId schoolId = "SchoolId";
            EventId eventId = Guid.NewGuid();

            sut.AddOrUpdate(new Subscription(schoolId, eventId,  "postbackUrl", "secret"));

            var result = sut.Get(schoolId, Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void ExistingIds()
        {
            var subscription = new Subscription("SchoolId", new Guid("00000000-1111-2222-3333-444444444444"),  "postbackUrl", "secret");
            
            sut.AddOrUpdate(subscription);

            var result = sut.Get("SchoolId", new Guid("00000000-1111-2222-3333-444444444444"));

            Assert.AreEqual(subscription, result);
        }
    }
}
