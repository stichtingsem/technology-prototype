using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System;
using System.Linq;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public class GetAllTests
    {
        SubscriptionsSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SubscriptionsSqlRepository();
        }

        [Test]
        public void EmptyRepo()
        {
            SchoolId schoolId = "SchoolId";

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void NonExistingSchoolId()
        {
            SchoolId schoolId = "SchoolId";
            EventId eventId = Guid.NewGuid();

            var subscription = new Subscription(schoolId, eventId,  "postbackUrl", "secret");

            sut.AddOrUpdate(subscription);

            var result = sut.GetAll("AnotherSchoolId");

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ExistingSchoolId()
        {
            SchoolId schoolId = "SchoolId";
            EventId eventId = Guid.NewGuid();

            var subscription = new Subscription(schoolId, eventId,  "postbackUrl", "secret");

            sut.AddOrUpdate(subscription);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ExistingSchoolIdMultiple()
        {
            SchoolId schoolId = "SchoolId";
            EventId eventId1 = Guid.NewGuid();
            EventId eventId2 = Guid.NewGuid();

            var subscription1 = new Subscription(schoolId, eventId1,  "postbackUrl", "secret");
            var subscription2 = new Subscription(schoolId, eventId2,  "postbackUrl", "secret");

            sut.AddOrUpdate(subscription1);
            sut.AddOrUpdate(subscription2);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(subscription1));
            Assert.IsTrue(result.Contains(subscription2));
        }
    }
}
