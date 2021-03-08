using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public class GetAllSubscriptionTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookSubscriptionInfoBuilder builder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void SingleSubscription()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            sut.Insert(webhookSubscriptionInfo);

            var result = sut.GetAllSubscriptions(webhookSubscriptionInfo.TenantId);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result.Single());
        }

        [Test]
        public void MultipleSubscriptions()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo1 = builder.Build();
            var webhookSubscriptionInfo2 = builder.WithTenantId(webhookSubscriptionInfo1.TenantId).Build();

            sut.Insert(webhookSubscriptionInfo1);
            sut.Insert(webhookSubscriptionInfo2);

            var result = sut.GetAllSubscriptions(webhookSubscriptionInfo1.TenantId);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo1.Id);
            sut.Delete(webhookSubscriptionInfo2.Id);

            result.Should().ContainEquivalentOf(webhookSubscriptionInfo1);
            result.Should().ContainEquivalentOf(webhookSubscriptionInfo2);
        }

        [Test]
        public void NoSubscriptions()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            var result = sut.GetAllSubscriptions(webhookSubscriptionInfo.TenantId);

            result.Should().BeEmpty();
        }

        [Test]
        public async Task SingleSubscriptionAsync()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            sut.Insert(webhookSubscriptionInfo);

            var result = await sut.GetAllSubscriptionsAsync(webhookSubscriptionInfo.TenantId);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            webhookSubscriptionInfo.Should().BeEquivalentTo(result.Single());
        }
    }
}
