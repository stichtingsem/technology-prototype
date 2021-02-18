using Domain.Generic;
using Domain.Schools;
using System.Collections.Generic;

namespace Domain.Webhooks
{
    public interface IWebhooksRepository
    {
        IEnumerable<Webhook> GetAll(SchoolId schoolId);

        Maybe<Webhook> Get(WebhookId webhookId, SchoolId schoolId);

        void Add(Webhook webhook);

        void Update(Webhook webhook);

        void Delete(WebhookId webhookId, SchoolId schoolId);
    }
}
