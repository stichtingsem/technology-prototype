using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class GetTests : WebhooksSqlRepositorySetup
    {
        [Test]
        public void WebhookDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();

            var result = sut.Get(webhookId, schoolId);

            Assert.IsNull(result);
        }

        [Test]
        public void SchoolIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            sut.Add(new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret"));

            var result = sut.Get(webhookId, RandomSchoolId());

            Assert.IsNull(result);
        }

        [Test]
        public void WebhookIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            sut.Add(new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret"));

            var result = sut.Get(RandomWebhookId(), schoolId);

            Assert.IsNull(result);
        }

        [Test]
        public void ValueEquality()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");
            
            sut.Add(webhook);

            WebhookId webhookIdCopy = new Guid(webhookId.ToString());
            SchoolId schoolIdCopy = new Guid(schoolId.ToString());

            var result = sut.Get(webhookIdCopy, schoolIdCopy);

            Assert.AreEqual(webhook, result);
        }
    }
}
