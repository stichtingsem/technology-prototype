using Domain.Schools;
using Domain.Webhooks;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositories
{
    public sealed class WebhooksSqlRepository : IWebhooksRepository
    {
        static readonly List<Webhook> webhooks = new List<Webhook>();

        public void Add(Webhook webhook)
        {
            webhooks.Add(webhook);
        }

        public void Delete(WebhookId webhookId, SchoolId schoolId)
        {
            webhooks.RemoveAll(webhook => webhook.IsFor(webhookId, schoolId));
        }

        public IEnumerable<Webhook> GetAll(SchoolId schoolId)
        {
            return webhooks.Where(webhook => webhook.IsFor(schoolId));
        }

        public Webhook Get(WebhookId webhookId, SchoolId schoolId)
        {
            return webhooks.SingleOrDefault(webhook => webhook.IsFor(webhookId, schoolId));
        }

        public void Update(Webhook webhook, SchoolId schoolId)
        {
            var index = webhooks.FindIndex(s => s == webhook && s.IsFor(schoolId));

            if (index >= 0)
                webhooks[index] = webhook;
        }
    }
}
