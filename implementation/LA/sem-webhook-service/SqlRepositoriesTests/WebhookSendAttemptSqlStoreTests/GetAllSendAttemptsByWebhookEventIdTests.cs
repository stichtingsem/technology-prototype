using FluentAssertions;
using NUnit.Framework;
using SqlRepositoriesTests.WebhookEventSqlStoreTests;
using SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSendAttemptSqlStoreTests
{
    public sealed class GetAllSendAttemptsByWebhookEventIdTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();

        readonly WebhookSendAttemptBuilder sendAttemptBuilder = new WebhookSendAttemptBuilder();
        readonly WebhookEventBuilder eventBuilder = new WebhookEventBuilder();
        readonly WebhookSubscriptionInfoBuilder subscriptionBuilder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void SingleWebhookSendAttempt()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt);

            var result = sut.GetAllSendAttemptsByWebhookEventId(webhookSendAttempt.TenantId, webhookEvent.Id);

            result.Single().Should().BeEquivalentTo(webhookSendAttempt);
        }

        [Test]
        public void MultipleWebhookSendAttempts()
        {
            var tenantId = Guid.NewGuid().GetHashCode();

            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.WithTenantId(tenantId).Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt1 = sendAttemptBuilder.WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt2 = sendAttemptBuilder.WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            
            var sut = factory.WebhookSendAttemptSqlStore;
            sut.Insert(webhookSendAttempt1);
            sut.Insert(webhookSendAttempt2);

            var result = sut.GetAllSendAttemptsByWebhookEventId(tenantId, webhookEvent.Id);

            result.Should().HaveCount(2);
            result.Should().ContainEquivalentOf(webhookSendAttempt1);
            result.Should().ContainEquivalentOf(webhookSendAttempt2);
        }

        [Test]
        public void NoneWebhookSendAttempt()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            var result = sut.GetAllSendAttemptsByWebhookEventId(webhookSendAttempt.TenantId, webhookEvent.Id);

            result.Should().BeEmpty();
        }

        [Test]
        public async Task SingleWebhookSendAttemptAsync()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            await sut.InsertAsync(webhookSendAttempt);

            var result = await sut.GetAllSendAttemptsByWebhookEventIdAsync(webhookSendAttempt.TenantId, webhookEvent.Id);

            result.Single().Should().BeEquivalentTo(webhookSendAttempt);
        }
    }
}
