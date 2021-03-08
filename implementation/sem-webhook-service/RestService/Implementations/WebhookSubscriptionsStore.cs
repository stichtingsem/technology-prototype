using Abp.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestService.Implementations
{
    class WebhookSubscriptionsStore : IWebhookSubscriptionsStore
    {
        private List<WebhookSubscriptionInfo> _subscriptions = new List<WebhookSubscriptionInfo>();

        public void Delete(Guid id)
        {
            var subscriptions = _subscriptions.Where(s => s.Id.Equals(id));
            if (subscriptions.Count() == 1)
            {
                _subscriptions.Remove(subscriptions.First());
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public WebhookSubscriptionInfo Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(int? tenantId)
        {
            var result = _subscriptions;
            if (tenantId.HasValue)
            {
                result = result.Where(s => (s.TenantId.HasValue && s.TenantId.Value.Equals(tenantId.Value))).ToList();
            }
            return result;
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(int? tenantId, string webhookName)
        {
            var result = _subscriptions;
            if (tenantId.HasValue)
            {
                result = result.Where(s => (s.TenantId.HasValue && s.TenantId.Value.Equals(tenantId.Value))).ToList();
            }
            if (!string.IsNullOrEmpty(webhookName))
            {
                result = result.Where(s => s.GetSubscribedWebhooks().Contains(webhookName)).ToList();
            }
            return result;
        }

        public Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(int? tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(int? tenantId, string webhookName)
        {
            var result = _subscriptions.Where(s => s.TenantId.Equals(tenantId) && s.GetSubscribedWebhooks().Contains(webhookName)).ToList();
            return Task.FromResult<List<WebhookSubscriptionInfo>>(result);
        }

        public Task<WebhookSubscriptionInfo> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(WebhookSubscriptionInfo webhookSubscription)
        {
            _subscriptions.Add(webhookSubscription);
        }

        public Task InsertAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            _subscriptions.Add(webhookSubscription);

            return Task.CompletedTask;
        }

        public bool IsSubscribed(int? tenantId, string webhookName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsSubscribedAsync(int? tenantId, string webhookName)
        {
            throw new NotImplementedException();
        }

        public void Update(WebhookSubscriptionInfo webhookSubscription)
        {
            this.Delete(webhookSubscription.Id);
            this.Insert(webhookSubscription);
        }

        public Task UpdateAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
