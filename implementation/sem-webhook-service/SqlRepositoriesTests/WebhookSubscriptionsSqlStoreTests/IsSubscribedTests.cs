using Abp.Webhooks;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public sealed class IsSubscribedTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookSubscriptionInfoBuilder builder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void ExistingTenantAndWebhook()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.IsSubscribed(webhookSubscriptionInfo.TenantId, "aWebhook");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void NonExistingWebhook()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.IsSubscribed(webhookSubscriptionInfo.TenantId, "aWebhook");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public void DifferentTenantId()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.IsSubscribed(tenantId: 123, "aWebhook");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public void DifferentWebhookName()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.IsSubscribed(webhookSubscriptionInfo.TenantId, "anotherWebhook");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public void NeitherTenantIdNorWebhookExist()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.IsSubscribed(tenantId: 123, "anotherWebhook");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ExistingTenantAndWebhookAsync()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            webhookSubscriptionInfo.SubscribeWebhook("aWebhook");

            await sut.InsertAsync(webhookSubscriptionInfo);

            var result = await sut.IsSubscribedAsync(webhookSubscriptionInfo.TenantId, "aWebhook");

            // Clean-up
            await sut.DeleteAsync(webhookSubscriptionInfo.Id);

            Assert.IsTrue(result);
        }
    }
}
