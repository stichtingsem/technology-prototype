using Domain.Events;
using Domain.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Webhooks
{
    public sealed class Webhook : IEquatable<Webhook>
    {
        public static Webhook Create(Guid tenantId, IEnumerable<Guid> eventIds, string postbackUrl, string secret) =>
            Create(WebhookId.Create(), tenantId, eventIds, postbackUrl, secret);

        public static Webhook Create(Guid webhookId, Guid tenantId, IEnumerable<Guid> eventIds, string postbackUrl, string secret) =>
            new Webhook(webhookId, tenantId, eventIds.Select(eventId => new EventId(eventId)), postbackUrl, secret);

        public bool HasEventId(EventId eventId) => EventIds.Any(ev => ev == eventId);

        public Webhook(WebhookId id, TenantId tenantId, IEnumerable<EventId> eventIds, PostbackUrl postbackUrl, Secret secret)
        {
            Id = id;
            TenantId = tenantId;
            EventIds = eventIds;
            PostbackUrl = postbackUrl;
            Secret = secret;
        }

        public WebhookId Id { get; }

        public TenantId TenantId { get; }

        public IEnumerable<EventId> EventIds { get; }
        
        public PostbackUrl PostbackUrl { get; }
        
        public Secret Secret { get; }

        public bool Equals(Webhook other) => other.Id == Id;

        public override bool Equals(object obj) => Equals(obj as Webhook);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Webhook left, Webhook right) => left.Equals(right);

        public static bool operator !=(Webhook left, Webhook right) => !(left == right);
    }
}
