using FluentAssertions;
using NUnit.Framework;
using SqlRepositoriesTests.WebhookEventSqlStoreTests;
using SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSendAttemptSqlStoreTests
{
    public sealed class UpdateTests
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

            webhookSendAttempt.Response = "modifiedResponse";

            sut.Update(webhookSendAttempt);

            var result = sut.Get(webhookSendAttempt.TenantId, webhookSendAttempt.Id);

            result.Response.Should().Be("modifiedResponse");
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

            webhookSendAttempt.Response = "modifiedResponse";

            await sut.UpdateAsync(webhookSendAttempt);

            var result = await sut.GetAsync(webhookSendAttempt.TenantId, webhookSendAttempt.Id);

            result.Response.Should().Be("modifiedResponse");
        }
    }
}

