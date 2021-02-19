using Domain.Events;
using Domain.Schools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Webhooks
{
    public sealed class Webhook : IEquatable<Webhook>
    {
        public static Webhook Create(Guid schoolId, IEnumerable<Guid> eventIds, string postbackUrl, string secret) =>
            Create(WebhookId.Create(), schoolId, eventIds, postbackUrl, secret);

        public static Webhook Create(Guid webhookId, Guid schoolId, IEnumerable<Guid> eventIds, string postbackUrl, string secret) =>
            new Webhook(webhookId, schoolId, eventIds.Select(eventId => new EventId(eventId)), postbackUrl, secret);

        public bool HasEventId(EventId eventId) => EventIds.Any(ev => ev == eventId);

        public Webhook(WebhookId id, SchoolId schoolId, IEnumerable<EventId> eventIds, PostbackUrl postbackUrl, Secret secret)
        {
            Id = id;
            SchoolId = schoolId;

            EventIds = eventIds;
            PostbackUrl = postbackUrl;
            Secret = secret;
        }

        public WebhookId Id { get; }

        public SchoolId SchoolId { get; }

        public IEnumerable<EventId> EventIds { get; }
        
        public PostbackUrl PostbackUrl { get; }
        
        public Secret Secret { get; }

        //public Result Convert<Result>(Func<WebhookId, SchoolId, IEnumerable<EventId>, PostbackUrl, Secret, Result> convert) => convert(Id, SchoolId, EventIds, PostbackUrl, Secret);

        public bool Equals(Webhook other) => other.Id == Id;

        public override bool Equals(object obj) => Equals(obj as Webhook);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Webhook left, Webhook right) => left.Equals(right);

        public static bool operator !=(Webhook left, Webhook right) => !(left == right);
    }
}
