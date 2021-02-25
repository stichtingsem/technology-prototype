using Domain.EventTypes;
using Domain.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Webhooks
{
    public sealed class Webhook : IEquatable<Webhook>
    {
        public static Webhook Create(Guid tenantId, IEnumerable<Guid> eventTypeIds, string postbackUrl, string secret) =>
            Create(WebhookId.Create(), tenantId, eventTypeIds, postbackUrl, secret);

        public static Webhook Create(Guid webhookId, Guid tenantId, IEnumerable<Guid> eventTypeIds, string postbackUrl, string secret) =>
            new Webhook(webhookId, tenantId, eventTypeIds.Select(eventTypeId => new EventTypeId(eventTypeId)), postbackUrl, secret);

        public bool HasEventType(EventTypeId eventTypeId) => EventTypeIds.Any(ev => ev == eventTypeId);

        public Webhook(WebhookId id, TenantId tenantId, IEnumerable<EventTypeId> eventTypeIds, PostbackUrl postbackUrl, Secret secret)
        {
            Id = id;
            TenantId = tenantId;
            EventTypeIds = eventTypeIds;
            PostbackUrl = postbackUrl;
            Secret = secret;
        }

        public WebhookId Id { get; }

        public TenantId TenantId { get; }

        public IEnumerable<EventTypeId> EventTypeIds { get; }
        
        public PostbackUrl PostbackUrl { get; }
        
        public Secret Secret { get; }

        public bool Equals(Webhook other) => other.Id == Id;

        public override bool Equals(object obj) => Equals(obj as Webhook);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Webhook left, Webhook right) => left.Equals(right);

        public static bool operator !=(Webhook left, Webhook right) => !(left == right);
    }
}
