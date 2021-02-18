using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using System.Collections.Generic;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class AddTests : WebhooksSqlRepositorySetup
    {
        [Test]
        public void AddWebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            sut.Get(webhookId, schoolId).Match
            (
                none: () => Assert.Fail(),
                some: (result) => Assert.AreEqual(result, webhook)
            );
        }
    }
}
