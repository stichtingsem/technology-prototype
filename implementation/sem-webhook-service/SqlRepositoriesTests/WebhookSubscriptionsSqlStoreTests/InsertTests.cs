using Abp.Webhooks;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public sealed class InsertTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookSubscriptionInfoBuilder builder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void PlainWebhookSubscriptionInfo()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            
            var webhookSubscriptionInfo = builder.Build();

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.Get(webhookSubscriptionInfo.Id);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);
            
            webhookSubscriptionInfo.Should().BeEquivalentTo(result);
        }

        [Test]
        public void WebhookSubscriptionInfoWithWebhooks()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook0");
            webhookSubscriptionInfo.SubscribeWebhook("aWebhook1");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.Get(webhookSubscriptionInfo.Id);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result);
        }

        [Test]
        public void WebhookSubscriptionInfoWithHeaders()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.AddWebhookHeader("headerKey0", "headerValue0_0");
            webhookSubscriptionInfo.AddWebhookHeader("headerKey0", "headerValue0_1");
            webhookSubscriptionInfo.AddWebhookHeader("headerKey1", "headerValue1_0");
            webhookSubscriptionInfo.AddWebhookHeader("headerKey1", "headerValue1_1");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.Get(webhookSubscriptionInfo.Id);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result);
        }

        [Test]
        public async Task InsertAsync()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            await sut.InsertAsync(webhookSubscriptionInfo);

            var result = await sut.GetAsync(webhookSubscriptionInfo.Id);

            // Clean-up
            await sut.DeleteAsync(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result);
        }
    }
}
