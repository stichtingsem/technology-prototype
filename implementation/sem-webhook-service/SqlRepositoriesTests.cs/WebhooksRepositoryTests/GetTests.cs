using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories;
using System;
using System.Collections.Generic;
using static SqlRepositoriesTests.WebhooksRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class GetTests
    {
        WebhooksSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new WebhooksSqlRepository();
        }

        [Test]
        public void webhookDoesNotExist()
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
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            sut.Add(new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret"));

            var result = sut.Get(webhookId, RandomSchoolId());

            Assert.IsNull(result);
        }

        [Test]
        public void webhookIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            sut.Add(new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret"));

            var result = sut.Get(RandomWebhookId(), schoolId);

            Assert.IsNull(result);
        }


        [Test]
        public void ValueEquality()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");
            
            sut.Add(webhook);

            WebhookId webhookIdCopy = new Guid(webhookId.ToString());
            SchoolId schoolIdCopy = new Guid(schoolId.ToString());

            var result = sut.Get(webhookIdCopy, schoolIdCopy);

            Assert.AreEqual(webhook, result);
        }
    }
}
