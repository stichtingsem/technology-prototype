using Domain.Events;
using Domain.Tenants;
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
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            sut.Get(webhookId, tenantId).Match
            (
                none: () => Assert.Fail(),
                some: (result) => Assert.AreEqual(result, webhook)
            );
        }
    }
}
