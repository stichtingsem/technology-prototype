using Abp.Dependency;
using Abp.Webhooks;
using Domain.EventTypes;
using Domain.Generic;
using Domain.Tenants;
using Domain.Webhooks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Implementations
{
    public class AbpWebhookRepository : IWebhooksRepository
    {
        private IWebhookSubscriptionsStore WebhookSubscriptionsStore;
        private IEventTypesRepository EventTypesRepository;
        private ITenantRepository TenantRepository;

        public AbpWebhookRepository(IEventTypesRepository eventTypesRepository, ITenantRepository tenantRepository)
        {
            this.WebhookSubscriptionsStore = IocManager.Instance.Resolve<IWebhookSubscriptionsStore>();
            this.EventTypesRepository = eventTypesRepository;
            this.TenantRepository = tenantRepository;
        }

        public void Add(Webhook webhook)
        {
            var webhooks = webhook.EventTypeIds.Select(e => this.EventTypesRepository.Get(e.Value)).ToList();
            var webhookNames = webhooks.Select(w => w.Match(null, m => m.Name.Value)).ToList();

            this.WebhookSubscriptionsStore.Insert(new WebhookSubscriptionInfo()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                TenantId = TenantRepository.GetTenantId(webhook.TenantId.Value),
                Webhooks = JsonConvert.SerializeObject(webhookNames),
                Secret="mySecret",
                WebhookUri=webhook.PostbackUrl.Value
            });
        }

        public void Delete(WebhookId webhookId, TenantId tenantId)
        {
            this.WebhookSubscriptionsStore.Delete(webhookId.Value);
        }

        public Maybe<Webhook> Get(WebhookId webhookId, TenantId tenantId)
        {
            var all = GetAll(tenantId);
            var result = all.Where(w => w.Id.Equals(webhookId));

            if (result.Count() == 0)
            {
                return new Maybe<Webhook>();
            }

            return new Maybe<Webhook>(result.First());
        }

        public IEnumerable<Webhook> GetAll(TenantId tenantId)
        {
            var subscriptions = this.WebhookSubscriptionsStore.GetAllSubscriptions(TenantRepository.GetTenantId(tenantId.Value));

            return subscriptions.Select(s =>
            {
                var webhooks = JsonConvert.DeserializeObject<List<string>>(s.Webhooks);
                var allEvents = this.EventTypesRepository.GetAll();
                var webhookIds = webhooks.Select(w => allEvents.FirstOrDefault(t => t.Name.Value.Equals(w)).Id).ToList();

                return new Webhook(
                    new WebhookId(s.Id),
                    tenantId,
                    webhookIds,
                    new PostbackUrl(s.WebhookUri),
                    new Secret("mySecret"));
            });
        }

        public void Update(Webhook webhook)
        {
            var webhooks = webhook.EventTypeIds.Select(e => this.EventTypesRepository.Get(e.Value)).ToList();
            var webhookNames = webhooks.Select(w => w.Match(null, m => m.Name.Value)).ToList();

            this.WebhookSubscriptionsStore.Update(new WebhookSubscriptionInfo()
            {
                Id = webhook.Id,
                CreationTime = DateTime.UtcNow,
                TenantId = TenantRepository.GetTenantId(webhook.TenantId.Value),
                Webhooks = JsonConvert.SerializeObject(webhookNames),
                Secret = "mySecret",
                WebhookUri = webhook.PostbackUrl.Value               
            });
        }
    }
}
