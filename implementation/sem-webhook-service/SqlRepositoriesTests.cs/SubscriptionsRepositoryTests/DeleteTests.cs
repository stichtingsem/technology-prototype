using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using NUnit.Framework;
using SqlRepositories;
using System;
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
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            sut.Delete(schoolId, eventId);

            Assert.Pass();
        }

        [Test]
        public void ExistingSubscription()
        {
            SchoolId schoolId = RandomSchoolId();
            EventId eventId = RandomEventId();

            var subscription = new Subscription(schoolId, eventId,  "postbackUrl", "secret");

            sut.AddOrUpdate(subscription);

            sut.Delete(schoolId, eventId);

            Assert.IsNull(sut.Get(schoolId, eventId));
        }
    }
}
