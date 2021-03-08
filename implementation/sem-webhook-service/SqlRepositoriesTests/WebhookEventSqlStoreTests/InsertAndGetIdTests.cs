using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookEventSqlStoreTests
{
    public sealed class InsertAndGetIdTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookEventBuilder builder = new WebhookEventBuilder();

        [Test]
        public void SingleWebhookEvent()
        {
            var sut = factory.WebhookEventsSqlStore;
            var webhookEvent = builder.Build();

            sut.InsertAndGetId(webhookEvent);

            var result = sut.Get(webhookEvent.TenantId, webhookEvent.Id);

            result.Should().BeEquivalentTo(webhookEvent);
        }

        [Test]
        public async Task SingleWebhookEventAsync()
        {
            var sut = factory.WebhookEventsSqlStore;
            var webhookEvent = builder.Build();

            await sut.InsertAndGetIdAsync(webhookEvent);

            var result = await sut.GetAsync(webhookEvent.TenantId, webhookEvent.Id);

            result.Should().BeEquivalentTo(webhookEvent);
        }

    }
}
