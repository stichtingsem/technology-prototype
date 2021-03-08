using Abp.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestService.Implementations
{
    class WebhookEventStore : IWebhookEventStore
    {
        List<WebhookEvent> _events = new List<WebhookEvent>();

        public WebhookEvent Get(int? tenantId, Guid id)
        {
            var results = _events;
            results = results.Where(e => e.Id.Equals(id)).ToList();
            if (tenantId.HasValue)
            {
                results = results.Where(e => (e.TenantId.HasValue && e.TenantId.Value.Equals(tenantId.Value))).ToList();
            }
            return results.FirstOrDefault();
        }

        public Task<WebhookEvent> GetAsync(int? tenantId, Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid InsertAndGetId(WebhookEvent webhookEvent)
        {
            _events.Add(webhookEvent);
            return webhookEvent.Id;
        }

        public Task<Guid> InsertAndGetIdAsync(WebhookEvent webhookEvent)
        {
            _events.Add(webhookEvent);
            return Task.FromResult(webhookEvent.Id);
        }
    }
}
