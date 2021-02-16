using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories;
using System.Collections.Generic;
using static SqlRepositoriesTests.WebhooksRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class AddTests
    {
        WebhooksSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new WebhooksSqlRepository();
        }

        [Test]
        public void Addwebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            Assert.AreEqual(webhook, sut.Get(webhookId, schoolId));
        }
    }
}
