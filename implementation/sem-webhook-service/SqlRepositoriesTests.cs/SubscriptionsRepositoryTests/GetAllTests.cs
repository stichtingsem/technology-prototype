using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using static SqlRepositoriesTests.SubscriptionsRepositoryTests.IdHelpers;

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
            SchoolId schoolId = RandomSchoolId();

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void NonExistingSchoolId()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var subscription = new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(subscription);

            var result = sut.GetAll(RandomSchoolId());

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ExistingSchoolId()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var subscription = new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(subscription);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ExistingSchoolIdMultipleSubscriptions()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds1 = ListOfRandomEventIds();
            IEnumerable<EventId> eventIds2 = ListOfRandomEventIds();

            var subscription1 = new Subscription(subscriptionId, schoolId, eventIds1, "postbackUrl", "secret");
            var subscription2 = new Subscription(subscriptionId, schoolId, eventIds2, "postbackUrl", "secret");

            sut.Add(subscription1);
            sut.Add(subscription2);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(subscription1));
            Assert.IsTrue(result.Contains(subscription2));
        }
    }
}
