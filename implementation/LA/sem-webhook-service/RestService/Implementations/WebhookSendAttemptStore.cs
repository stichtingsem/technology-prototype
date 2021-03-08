using Abp.Application.Services.Dto;
using Abp.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestService.Implementations
{
    class WebhookSendAttemptStore : IWebhookSendAttemptStore
    {
        private List<WebhookSendAttempt> _sendAttempts = new List<WebhookSendAttempt>();
        public WebhookSendAttempt Get(int? tenantId, Guid id)
        {
            var result = _sendAttempts.Where(a => a.TenantId.Equals(tenantId) && a.Id.Equals(id));
            return result.FirstOrDefault();
        }

        public IPagedResult<WebhookSendAttempt> GetAllSendAttemptsBySubscriptionAsPagedList(int? tenantId, Guid subscriptionId, int maxResultCount, int skipCount)
        {
            var result = _sendAttempts;
            if (tenantId.HasValue)
            {
                result = result.Where(a => (a.TenantId.HasValue && a.TenantId.Value.Equals(tenantId.Value))).ToList();
            }
            result = result.Where(a => a.WebhookSubscriptionId.Equals(subscriptionId)).ToList();
            int count = result.Count;
            result = result.Skip(skipCount).Take(maxResultCount).ToList();

            return new PagedResultDto<WebhookSendAttempt>(count, result);
        }

        public Task<IPagedResult<WebhookSendAttempt>> GetAllSendAttemptsBySubscriptionAsPagedListAsync(int? tenantId, Guid subscriptionId, int maxResultCount, int skipCount)
        {
            throw new NotImplementedException();
        }

        public List<WebhookSendAttempt> GetAllSendAttemptsByWebhookEventId(int? tenantId, Guid webhookEventId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WebhookSendAttempt>> GetAllSendAttemptsByWebhookEventIdAsync(int? tenantId, Guid webhookEventId)
        {
            throw new NotImplementedException();
        }

        public Task<WebhookSendAttempt> GetAsync(int? tenantId, Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetSendAttemptCount(int? tenantId, Guid webhookId, Guid webhookSubscriptionId)
        {
            var result = _sendAttempts.Where(a => a.TenantId.Equals(tenantId) && a.WebhookSubscriptionId.Equals(webhookSubscriptionId) && a.WebhookEventId.Equals(webhookId)).Count();
            return result;
        }

        public Task<int> GetSendAttemptCountAsync(int? tenantId, Guid webhookId, Guid webhookSubscriptionId)
        {
            throw new NotImplementedException();
        }

        public bool HasXConsecutiveFail(int? tenantId, Guid subscriptionId, int searchCount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasXConsecutiveFailAsync(int? tenantId, Guid subscriptionId, int searchCount)
        {
            throw new NotImplementedException();
        }

        public void Insert(WebhookSendAttempt webhookSendAttempt)
        {
            webhookSendAttempt.Id = Guid.NewGuid();
            _sendAttempts.Add(webhookSendAttempt);
        }

        public Task InsertAsync(WebhookSendAttempt webhookSendAttempt)
        {
            throw new NotImplementedException();
        }

        public void Update(WebhookSendAttempt webhookSendAttempt)
        {
            _sendAttempts.Remove(Get(webhookSendAttempt.TenantId, webhookSendAttempt.Id));
            _sendAttempts.Add(webhookSendAttempt);
        }

        public Task UpdateAsync(WebhookSendAttempt webhookSendAttempt)
        {
            throw new NotImplementedException();
        }
    }
}
