using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories;
using System.Collections.Generic;
using System.Linq;
using static SqlRepositoriesTests.WebhooksRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class UpdateTests
    {
        WebhooksSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new WebhooksSqlRepository();
        }

        [Test]
        public void Updatewebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var webhook1 = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");
            var webhook2 = new Webhook(webhookId, schoolId, eventIds, "updatedUrl", "updatedSecret");

            sut.Add(webhook1);

            sut.Update(webhook2, schoolId);

            Assert.AreEqual(1, sut.GetAll(schoolId).Count());
        }
    }
}
