using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using static SqlRepositoriesTests.WebhooksRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class GetAllTests
    {
        WebhooksSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new WebhooksSqlRepository();
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
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            var result = sut.GetAll(RandomSchoolId());

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ExistingSchoolId()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void ExistingSchoolIdMultiplewebhooks()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds1 = ListOfRandomEventIds();
            IEnumerable<EventId> eventIds2 = ListOfRandomEventIds();

            var webhook1 = new Webhook(webhookId, schoolId, eventIds1, "postbackUrl", "secret");
            var webhook2 = new Webhook(webhookId, schoolId, eventIds2, "postbackUrl", "secret");

            sut.Add(webhook1);
            sut.Add(webhook2);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(webhook1));
            Assert.IsTrue(result.Contains(webhook2));
        }
    }
}
