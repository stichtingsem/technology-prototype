using Castle.Facilities.TypedFactory;
using NUnit.Framework;
using System.Threading.Tasks;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public class UpdateTests
    {
        readonly RepositoryFactory factory = new RepositoryFactory();
        readonly WebhookSubscriptionInfoBuilder builder = new WebhookSubscriptionInfoBuilder();

        [Test]
        public void ExistingWebhookSubscriptionInfo()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            sut.Insert(webhookSubscriptionInfo);

            webhookSubscriptionInfo.Secret = "someOtherSecret";

            sut.Update(webhookSubscriptionInfo);

            var result = sut.Get(webhookSubscriptionInfo.Id);

            // Clean-up
            sut.Delete(webhookSubscriptionInfo.Id);

            Assert.AreEqual("someOtherSecret", result.Secret);
        }

        [Test]
        public async Task ExistingWebhookSubscriptionInfoAsync()
        {
            var sut = factory.WebhookSubscriptionsSqlStore;
            var webhookSubscriptionInfo = builder.Build();

            await sut.InsertAsync(webhookSubscriptionInfo);

            webhookSubscriptionInfo.Secret = "someOtherSecret";

            await sut.UpdateAsync(webhookSubscriptionInfo);

            var result = await sut.GetAsync(webhookSubscriptionInfo.Id);

            // Clean-up
            await sut.DeleteAsync(webhookSubscriptionInfo.Id);

            Assert.AreEqual("someOtherSecret", result.Secret);
        }
    }
}