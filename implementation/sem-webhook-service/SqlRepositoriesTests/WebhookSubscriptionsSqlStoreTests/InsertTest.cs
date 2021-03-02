using Abp.Webhooks;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public class InsertTest
    {
        private readonly RepositoryFactory factory = new RepositoryFactory();

        private static WebhookSubscriptionInfo CreateWebhookSubscriptionInfo() => new WebhookSubscriptionInfo
        {
            Id = Guid.NewGuid(),
            TenantId = 1,
            CreationTime = new DateTime(2020, 01, 01),
            IsActive = true,
            WebhookUri = "https://some.uri",
            Secret = "aSecret"
        };

        [Test]
        public void PlainWebhookSubscriptionInfo()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            
            var webhookSubscriptionInfo = CreateWebhookSubscriptionInfo();

            sut.Insert(webhookSubscriptionInfo);

            webhookSubscriptionInfo.Should().BeEquivalentTo(sut.Get(webhookSubscriptionInfo.Id));
        }

        [Test]
        public void WebhookSubscriptionInfoWithWebhooks()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = CreateWebhookSubscriptionInfo();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook0");
            webhookSubscriptionInfo.SubscribeWebhook("aWebhook1");

            sut.Insert(webhookSubscriptionInfo);

            webhookSubscriptionInfo.Should().BeEquivalentTo(sut.Get(webhookSubscriptionInfo.Id));
        }
    }
}
