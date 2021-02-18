using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using System.Collections.Generic;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class DeleteTests : WebhooksSqlRepositorySetup
    {
        [Test]
        public void NonExistingWebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();

            sut.Delete(webhookId, schoolId);

            Assert.Pass();
        }

        [Test]
        public void ExistingWebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            sut.Delete(webhookId, schoolId);

            sut.Get(webhookId, schoolId).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }
    }
}
