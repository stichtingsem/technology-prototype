using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class GetAllTests : WebhooksSqlRepositorySetup
    {
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
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

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
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void MultipleWebhooksForSchool()
        {
            WebhookId webhookId1 = RandomWebhookId();
            WebhookId webhookId2 = RandomWebhookId();

            SchoolId schoolId = RandomSchoolId();

            IEnumerable<EventId> eventIds1 = ListOfDistinctEventIds();
            IEnumerable<EventId> eventIds2 = ListOfDistinctEventIds();

            var webhook1 = new Webhook(webhookId1, schoolId, eventIds1, "postbackUrl", "secret");
            var webhook2 = new Webhook(webhookId2, schoolId, eventIds2, "postbackUrl", "secret");

            sut.Add(webhook1);
            sut.Add(webhook2);

            var result = sut.GetAll(schoolId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(webhook1));
            Assert.IsTrue(result.Contains(webhook2));
        }
    }
}
