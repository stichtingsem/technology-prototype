using Abp.Webhooks;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public class GetAllSubscriptionsWithWebhookNameTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookSubscriptionInfoBuilder builder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void Single()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();
            webhookSubscriptionInfo.SubscribeWebhook("aWebhookName");

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.GetAllSubscriptions(webhookSubscriptionInfo.TenantId, "aWebhookName");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result.Single());
        }

        [Test]
        public async Task SingleAsync()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();
            webhookSubscriptionInfo.SubscribeWebhook("aWebhookName");

            sut.Insert(webhookSubscriptionInfo);

            var result = await sut.GetAllSubscriptionsAsync(webhookSubscriptionInfo.TenantId, "aWebhookName");

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result.Single());
        }
    }
}
