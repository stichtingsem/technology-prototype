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

            sut.Get(webhookId, schoolId).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }

        [Test]
        public void SchoolIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            sut.Add(new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret"));

            sut.Get(webhookId, RandomSchoolId()).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }

        [Test]
        public void WebhookIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            sut.Add(new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret"));

            sut.Get(RandomWebhookId(), schoolId).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
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

            sut.Get(webhookIdCopy, schoolIdCopy).Match
            (
                none: () => Assert.Fail(),
                some: (result) => Assert.AreEqual(result, webhook)
            );
        }
    }
}
