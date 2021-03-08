using FluentAssertions;
using NUnit.Framework;
using SqlRepositoriesTests.WebhookEventSqlStoreTests;
using SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSendAttemptSqlStoreTests
{
    public sealed class HasXConsecutiveFailTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();

        readonly WebhookSendAttemptBuilder sendAttemptBuilder = new WebhookSendAttemptBuilder();
        readonly WebhookEventBuilder eventBuilder = new WebhookEventBuilder();
        readonly WebhookSubscriptionInfoBuilder subscriptionBuilder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void SuccessfulWebhookSendAttempt()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt);

            var result = sut.HasXConsecutiveFail(webhookSendAttempt.TenantId, webhookSubscription.Id, 1);

            result.Should().BeFalse();
        }

        [Test]
        public void UnsuccessfulWebhookSendAttempt()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.WithHttpStatusCode(HttpStatusCode.BadRequest).Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt);

            var result = sut.HasXConsecutiveFail(webhookSendAttempt.TenantId, webhookSubscription.Id, 1);

            result.Should().BeTrue();
        }

        [Test]
        public void MultipleWebhookSendAttempts()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var tenantId = Guid.NewGuid().GetHashCode();
            sendAttemptBuilder.WithTenantId(tenantId);

            var badRequestAttempt = sendAttemptBuilder.WithHttpStatusCode(HttpStatusCode.BadRequest).Build(webhookEvent.Id, webhookSubscription.Id);
            var succesfulAttempt = sendAttemptBuilder.WithHttpStatusCode(HttpStatusCode.OK).Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(badRequestAttempt);
            sut.Insert(succesfulAttempt);

            var result = sut.HasXConsecutiveFail(tenantId, webhookSubscription.Id, 2);
            
            result.Should().BeFalse();
        }

        [Test]
        public void MoreThanXAttempts()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var tenantId = Guid.NewGuid().GetHashCode();
            sendAttemptBuilder.WithTenantId(tenantId);

            var succesfulAttempt = sendAttemptBuilder.WithHttpStatusCode(HttpStatusCode.OK).WithCreationTime(new DateTime(2020, 02, 19)).Build(webhookEvent.Id, webhookSubscription.Id);
            var notFoundAttempt = sendAttemptBuilder.WithHttpStatusCode(HttpStatusCode.NotFound).WithCreationTime(new DateTime(2020, 02, 20)).Build(webhookEvent.Id, webhookSubscription.Id);
            var badRequestAttempt = sendAttemptBuilder.WithHttpStatusCode(HttpStatusCode.BadRequest).WithCreationTime(new DateTime(2020, 02, 21)).Build(webhookEvent.Id, webhookSubscription.Id);
            
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(badRequestAttempt);
            sut.Insert(notFoundAttempt);
            sut.Insert(succesfulAttempt);

            var result = sut.HasXConsecutiveFail(tenantId, webhookSubscription.Id, 2);

            result.Should().BeTrue();
        }

        [Test]
        public async Task SuccessfulWebhookSendAttemptAsync()
        {
            var webhookEvent = eventBuilder.Build();
            factory.WebhookEventsSqlStore.InsertAndGetId(webhookEvent);

            var webhookSubscription = subscriptionBuilder.Build();
            factory.WebhookSubscriptionsSqlStore.Insert(webhookSubscription);

            var webhookSendAttempt = sendAttemptBuilder.Build(webhookEvent.Id, webhookSubscription.Id);
            var sut = factory.WebhookSendAttemptSqlStore;

            sut.Insert(webhookSendAttempt);

            var result = await sut.HasXConsecutiveFailAsync(webhookSendAttempt.TenantId, webhookSubscription.Id, 1);

            result.Should().BeFalse();
        }
    }
}
