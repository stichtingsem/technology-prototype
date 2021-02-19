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

        private readonly IEnumerable<EventId> eventIds;
        private readonly PostbackUrl postbackUrl;
        private readonly Secret secret;

        public Webhook(WebhookId id, SchoolId schoolId, IEnumerable<EventId> eventIds, PostbackUrl postbackUrl, Secret secret)
        {
            Id = id;
            SchoolId = schoolId;

            this.eventIds = eventIds;
            this.postbackUrl = postbackUrl;
            this.secret = secret;
        }

        public WebhookId Id { get; }

        public SchoolId SchoolId { get; }

        public Result Convert<Result>(Func<WebhookId, SchoolId, IEnumerable<EventId>, PostbackUrl, Secret, Result> convert) => convert(Id, SchoolId, eventIds, postbackUrl, secret);

        public bool Equals(Webhook other) => other.Id == Id;

        public override bool Equals(object obj) => Equals(obj as Webhook);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Webhook left, Webhook right) => left.Equals(right);

        public static bool operator !=(Webhook left, Webhook right) => !(left == right);
    }
}
