using Domain.Events;
using Domain.Tenants;
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
            TenantId tenantId = RandomTenantId();

            sut.Delete(webhookId, tenantId);

            Assert.Pass();
        }

        [Test]
        public void ExistingWebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            sut.Delete(webhookId, tenantId);

            sut.Get(webhookId, tenantId).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }
    }
}
