using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System;
using static SqlRepositoriesTests.SubscriptionsRepositoryTests.IdHelpers;

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
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            var result = sut.Get(schoolId, eventId);

            Assert.IsNull(result);
        }

        [Test]
        public void SchoolIdDoesNotExist()
        {
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            sut.AddOrUpdate(new Subscription(schoolId, eventId,  "postbackUrl", "secret"));

            var result = sut.Get(RandomSchoolId(), eventId);

            Assert.IsNull(result);
        }

        [Test]
        public void EventIdDoesNotExist()
        {
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            sut.AddOrUpdate(new Subscription(schoolId, eventId,  "postbackUrl", "secret"));

            var result = sut.Get(schoolId, Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void ValueEquality()
        {
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            var subscription = new Subscription(schoolId, eventId, "postbackUrl", "secret");
            
            sut.AddOrUpdate(subscription);

            SchoolId schoolIdCopy = new Guid(schoolId.ToString());
            EventId eventIdCopy = new Guid(eventId.ToString());

            var result = sut.Get(schoolIdCopy, eventIdCopy);

            Assert.AreEqual(subscription, result);
        }
    }
}
