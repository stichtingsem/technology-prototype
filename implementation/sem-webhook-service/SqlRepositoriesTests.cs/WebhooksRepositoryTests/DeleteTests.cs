using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using NUnit.Framework;
using SqlRepositories;
using System.Collections.Generic;
using static SqlRepositoriesTests.WebhooksRepositoryTests.IdHelpers;

namespace SqlRepositoriesTests.WebhooksRepositoryTests
{
    public class DeleteTests
    {
        WebhooksSqlRepository sut;

        [SetUp]
        public void SetUp()
        {
            sut = new WebhooksSqlRepository();
        }

        [Test]
        public void NonExistingwebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();

            sut.Delete(webhookId, schoolId);

            Assert.Pass();
        }

        [Test]
        public void Existingwebhook()
        {
            WebhookId webhookId = RandomWebhookId();
            SchoolId schoolId = RandomSchoolId();
            IEnumerable<EventId> eventIds = ListOfRandomEventIds();

            var webhook = new Webhook(webhookId, schoolId, eventIds, "postbackUrl", "secret");

            sut.Add(webhook);

            sut.Delete(webhookId, schoolId);

            Assert.IsNull(sut.Get(webhookId, schoolId));
        }
    }
}
