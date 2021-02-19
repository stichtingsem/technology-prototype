using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class UpdateTests : WebhooksSqlRepositorySetup
    {
        [Test]
        public void UpdateWebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfDistinctEventIds();

            var webhook1 = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");
            var webhook2 = new Webhook(webhookId, schoolId, eventIds, "updatedUrl", "updatedSecret");

            sut.Add(webhook1);

            sut.Update(webhook2);

            Assert.AreEqual(1, sut.GetAll(schoolId).Count());

            var result = sut.Get(webhookId, schoolId);
            result.Match
            (
                none: Assert.Fail,
                some: (value) =>
                {
                    Assert.IsTrue("updatedUrl" == value.PostbackUrl);
                    Assert.IsTrue("updatedSecret" == value.Secret);
                }
            );
        }
    }
}
