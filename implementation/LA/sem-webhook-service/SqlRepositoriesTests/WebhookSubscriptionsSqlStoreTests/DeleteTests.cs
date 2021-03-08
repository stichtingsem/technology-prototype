using NUnit.Framework;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public sealed class DeleteTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookSubscriptionInfoBuilder builder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void DeleteAnExistingWebhook()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();
            
            sut.Insert(webhookSubscriptionInfo);
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.IsNull(sut.Get(webhookSubscriptionInfo.Id));
        }

        [Test]
        public async Task DeleteAnExistingWebhookAsync()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;

            var webhookSubscriptionInfo = builder.Build();

            await sut.InsertAsync(webhookSubscriptionInfo);
            await sut.DeleteAsync(webhookSubscriptionInfo.Id);

            Assert.IsNull(await sut.GetAsync(webhookSubscriptionInfo.Id));
        }
    }
}
