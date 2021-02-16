using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System;
using System.Collections.Generic;
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
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();

            var result = sut.Get(subscriptionId, schoolId);

            Assert.IsNull(result);
        }

        [Test]
        public void SchoolIdDoesNotExist()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            sut.Add(new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret"));

            var result = sut.Get(subscriptionId, RandomSchoolId());

            Assert.IsNull(result);
        }

        [Test]
        public void SubscriptionIdDoesNotExist()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            sut.Add(new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret"));

            var result = sut.Get(RandomSubscriptionId(), schoolId);

            Assert.IsNull(result);
        }


        [Test]
        public void ValueEquality()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var subscription = new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret");
            
            sut.Add(subscription);

            SubscriptionId subscriptionIdCopy = new Guid(subscriptionId.ToString());
            SchoolId schoolIdCopy = new Guid(schoolId.ToString());

            var result = sut.Get(subscriptionIdCopy, schoolIdCopy);

            Assert.AreEqual(subscription, result);
        }
    }
}
