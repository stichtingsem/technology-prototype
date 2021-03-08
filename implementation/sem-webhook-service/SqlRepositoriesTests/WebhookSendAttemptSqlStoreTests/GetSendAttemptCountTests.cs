using FluentAssertions;
using NUnit.Framework;
using SqlRepositoriesTests.WebhookEventSqlStoreTests;
using SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSendAttemptSqlStoreTests
{
    public sealed class GetSendAttemptCountTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();

        readonly WebhookSendAttemptBuilder sendAttemptBuilder = new WebhookSendAttemptBuilder();
        readonly WebhookEventBuilder eventBuilder = new WebhookEventBuilder();
        readonly WebhookSubscriptionInfoBuilder subscriptionBuilder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void Single()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt);

            var result = sut.GetSendAttemptCount(webhookSendAttempt.TenantId, webhookEvent.Id, webhookSubscription.Id);

            result.Should().Be(1);
        }

        [Test]
        public void None()
        {
            var webhookEvent = eventBuilder.Build();
            var webhookSubscription = subscriptionBuilder.Build();
            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);

            var sut = factory.WebhookSendAttemptSqlStore;

            var result = sut.GetSendAttemptCount(webhookSendAttempt.TenantId, webhookEvent.Id, webhookSubscription.Id);

            result.Should().Be(0);
        }

        [Test]
        public void Multiple()
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

            var result = sut.GetSendAttemptCount(tenantId, webhookEvent.Id, webhookSubscription.Id);

            result.Should().Be(2);
        }

        [Test]
        public async Task SingleAsync()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            await sut.InsertAsync(webhookSendAttempt);

            var result = await sut.GetSendAttemptCountAsync(webhookSendAttempt.TenantId, webhookEvent.Id, webhookSubscription.Id);

            result.Should().Be(1);
        }
    }
}
