using Domain.Generic;
using Domain.Tenants;
using System.Collections.Generic;

namespace Domain.Webhooks
{
    public interface IWebhooksRepository
    {
        IEnumerable<Webhook> GetAll(TenantId tenantId);

        Maybe<Webhook> Get(WebhookId webhookId, TenantId tenantId);

        void Add(Webhook webhook);

        void Update(Webhook webhook);

        void Delete(WebhookId webhookId, TenantId tenantId);
    }
}
