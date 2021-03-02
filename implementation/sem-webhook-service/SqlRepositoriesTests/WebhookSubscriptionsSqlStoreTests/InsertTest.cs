using Abp.Webhooks;
using NUnit.Framework;
using System;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public class InsertTest
    {
        private readonly RepositoryFactory factory = new RepositoryFactory();

        [Test]
        public void PlainWebhookSubscriptionInfo()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = new WebhookSubscriptionInfo
            { 
                Id = Guid.NewGuid(),
                TenantId = 1,
                CreationTime = DateTime.UtcNow,
                IsActive = true,
                WebhookUri = "https://some.uri",
                Secret = "aSecret"
            };

            sut.Insert(webhookSubscriptionInfo);

            Assert.Pass();
        }
    }
}
