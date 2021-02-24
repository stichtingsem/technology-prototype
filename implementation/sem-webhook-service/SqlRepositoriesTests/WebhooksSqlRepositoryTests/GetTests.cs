using Domain.EventTypes;
using Domain.Tenants;
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
            TenantId tenantId = RandomTenantId();

            sut.Get(webhookId, tenantId).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }

        [Test]
        public void TenantIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventTypeId> eventIds = ListOfDistinctEventIds();

            sut.Add(new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret"));

            sut.Get(webhookId, RandomTenantId()).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }

        [Test]
        public void WebhookIdDoesNotExist()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventTypeId> eventIds = ListOfDistinctEventIds();

            sut.Add(new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret"));

            sut.Get(RandomWebhookId(), tenantId).Match
            (
                none: () => Assert.Pass(),
                some: (result) => Assert.Fail()
            );
        }

        [Test]
        public void ValueEquality()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventTypeId> eventIds = ListOfDistinctEventIds();

            var webhook = new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret");
            
            sut.Add(webhook);

            WebhookId webhookIdCopy = new Guid(webhookId.ToString());
            TenantId tenantIdCopy = new Guid(tenantId.ToString());

            sut.Get(webhookIdCopy, tenantIdCopy).Match
            (
                none: () => Assert.Fail(),
                some: (result) => Assert.AreEqual(result, webhook)
            );
        }
    }
}
