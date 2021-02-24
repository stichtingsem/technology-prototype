using Domain.Events;
using Domain.Tenants;
using Domain.Webhooks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class UpdateTests : WebhooksSqlRepositorySetup
    {
        [Test]
        public void UpdateWebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            TenantId tenantId = RandomTenantId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook1 = new Webhook(webhookId, tenantId, eventIds, "postbackUrl", "secret");
            var webhook2 = new Webhook(webhookId, tenantId, eventIds, "updatedUrl", "updatedSecret");

            sut.Add(webhook1);

            sut.Update(webhook2);

            Assert.AreEqual(1, sut.GetAll(tenantId).Count());

            var result = sut.Get(webhookId, tenantId);
            result.Match
            (
                none: Assert.Fail,
                some: (value) =>
                {
                    Assert.IsTrue("updatedUrl" == value.PostbackUrl);
                    Assert.IsTrue("updatedSecret" == value.Secret);
                }
            );
        }
    }
}
