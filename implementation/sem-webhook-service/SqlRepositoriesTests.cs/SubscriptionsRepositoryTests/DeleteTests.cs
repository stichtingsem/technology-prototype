﻿using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System.Collections.Generic;
using static SqlRepositoriesTests.SubscriptionsRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public class DeleteTests
    {
        SubscriptionsSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SubscriptionsSqlRepository();
        }

        [Test]
        public void NonExistingSubscription()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();

            sut.Delete(subscriptionId, schoolId);

            Assert.Pass();
        }

        [Test]
        public void ExistingSubscription()
        {
            SubscriptionId subscriptionId = RandomSubscriptionId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var subscription = new Subscription(subscriptionId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(subscription);

            sut.Delete(subscriptionId, schoolId);

            Assert.IsNull(sut.Get(subscriptionId, schoolId));
        }
    }
}
