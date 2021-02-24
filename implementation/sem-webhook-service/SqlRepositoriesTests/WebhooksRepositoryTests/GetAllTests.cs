using Domain.Events;
using Domain.Tenants;
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
            TenantId tetantId = RandomTenantId();

            var result = sut.GetAll(tetantId);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void NonExistingTenantId()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            var result = sut.GetAll(RandomTenantId());

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ExistingTenantId()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            var result = sut.GetAll(tenantId);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void MultipleWebhooksForTenant()
        {
            WebhookId webhookId1 = RandomWebhookId();
            WebhookId webhookId2 = RandomWebhookId();

            TenantId tenantId = RandomTenantId();

            IEnumerable<EventId> eventIds1 = ListOfDistinctEventIds();
            IEnumerable<EventId> eventIds2 = ListOfDistinctEventIds();

            var webhook1 = new Webhook(webhookId1, tenantId, eventIds1, "postbackUrl", "secret");
            var webhook2 = new Webhook(webhookId2, tenantId, eventIds2, "postbackUrl", "secret");

            sut.Add(webhook1);
            sut.Add(webhook2);

            var result = sut.GetAll(tenantId);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(webhook1));
            Assert.IsTrue(result.Contains(webhook2));
        }
    }
}
