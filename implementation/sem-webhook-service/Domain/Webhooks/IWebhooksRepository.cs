using Domain.Schools;
using System.Collections.Generic;

namespace Domain.Webhooks
{
    public interface IWebhooksRepository
    {
        IEnumerable<Webhook> GetAll(SchoolId schoolId);

        Webhook Get(WebhookId webhookId, SchoolId schoolId);

        void Add(Webhook webhook);

        void Update(Webhook webhook, SchoolId schoolId);

        void Delete(WebhookId webhook, SchoolId schoolId);
    }
}
