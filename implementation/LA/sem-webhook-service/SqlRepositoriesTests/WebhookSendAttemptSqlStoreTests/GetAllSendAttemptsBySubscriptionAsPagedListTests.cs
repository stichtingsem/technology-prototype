using FluentAssertions;
using NUnit.Framework;
using SqlRepositoriesTests.WebhookEventSqlStoreTests;
using SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests;
using System;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSendAttemptSqlStoreTests
{
    public sealed class GetAllSendAttemptsBySubscriptionAsPagedListTests
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

            var result = sut.GetAllSendAttemptsBySubscriptionAsPagedList(webhookSendAttempt.TenantId, webhookSubscription.Id, 1, 0);

            result.TotalCount.Should().Be(1);
            result.Items.Should().ContainEquivalentOf(webhookSendAttempt);
        }

        [Test]
        public void Multiple()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var tenantId = Guid.NewGuid().GetHashCode();
            var webhookSendAttempt1 = sendAttemptBuilder.WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt2 = sendAttemptBuilder.WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);

            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt1);
            sut.Insert(webhookSendAttempt2);

            var result = sut.GetAllSendAttemptsBySubscriptionAsPagedList(tenantId, webhookSubscription.Id, 2, 0);

            result.TotalCount.Should().Be(2);
            result.Items.Should().ContainEquivalentOf(webhookSendAttempt1);
            result.Items.Should().ContainEquivalentOf(webhookSendAttempt2);
        }

        [Test]
        public void None()
        {
            var webhookEvent = eventBuilder.Build();
            var webhookSubscription = subscriptionBuilder.Build();
            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            
            var sut = factory.WebhookSendAttemptSqlStore;

            var result = sut.GetAllSendAttemptsBySubscriptionAsPagedList(webhookSendAttempt.TenantId, webhookSubscription.Id, 1, 0);

            result.TotalCount.Should().Be(0);
            result.Items.Should().BeEmpty();
        }

        [Test]
        public void MultipleWithPagination()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var tenantId = Guid.NewGuid().GetHashCode();
            var webhookSendAttempt1 = sendAttemptBuilder.WithCreationTime(new DateTime(2020, 02, 20)).WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt2 = sendAttemptBuilder.WithCreationTime(new DateTime(2020, 02, 21)).WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt3 = sendAttemptBuilder.WithCreationTime(new DateTime(2020, 02, 22)).WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt4 = sendAttemptBuilder.WithCreationTime(new DateTime(2020, 02, 23)).WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt5 = sendAttemptBuilder.WithCreationTime(new DateTime(2020, 02, 24)).WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);
            var webhookSendAttempt6 = sendAttemptBuilder.WithCreationTime(new DateTime(2020, 02, 25)).WithTenantId(tenantId).Build(webhookEvent.Id, webhookSubscription.Id);

            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt1);
            sut.Insert(webhookSendAttempt2);
            sut.Insert(webhookSendAttempt3);
            sut.Insert(webhookSendAttempt4);
            sut.Insert(webhookSendAttempt5);
            sut.Insert(webhookSendAttempt6);

            var result = sut.GetAllSendAttemptsBySubscriptionAsPagedList(tenantId, webhookSubscription.Id, 2, 1);

            result.TotalCount.Should().Be(6);
            result.Items.Should().HaveCount(2);
            result.Items.Should().ContainEquivalentOf(webhookSendAttempt4);
            result.Items.Should().ContainEquivalentOf(webhookSendAttempt5);
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

            var result = await sut.GetAllSendAttemptsBySubscriptionAsPagedListAsync(webhookSendAttempt.TenantId, webhookSubscription.Id, 1, 0);

            result.TotalCount.Should().Be(1);
            result.Items.Should().ContainEquivalentOf(webhookSendAttempt);
        }
    }
}
