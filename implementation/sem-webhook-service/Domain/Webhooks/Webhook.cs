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
        private readonly WebhookId webhookId;
        private readonly SchoolId schoolId;
        private readonly PostbackUrl postbackUrl;
        private readonly Secret secret;

        public Webhook(WebhookId webhookId, SchoolId schoolId, IEnumerable<EventId> eventIds, PostbackUrl postbackUrl, Secret secret)
        {
            this.eventIds = eventIds;
            this.webhookId = webhookId;
            this.schoolId = schoolId;
            this.postbackUrl = postbackUrl;
            this.secret = secret;
        }

        public Result Convert<Result>(Func<WebhookId, IEnumerable<EventId>, PostbackUrl, Secret, Result> convert) => convert(webhookId, eventIds, postbackUrl, secret);

        public bool Equals(Webhook other) => other.IsFor(webhookId);

        public override bool Equals(object obj) => Equals(obj as Webhook);

        public override int GetHashCode() => webhookId.GetHashCode();

        public static bool operator ==(Webhook left, Webhook right) => left.Equals(right);

        public static bool operator !=(Webhook left, Webhook right) => !(left == right);

        public bool IsFor(WebhookId webhookId, SchoolId schoolId) => IsFor(webhookId) && IsFor(schoolId);

        public bool IsFor(SchoolId schoolId) => this.schoolId == schoolId;

        public bool IsFor(WebhookId webhookId) => this.webhookId == webhookId;
    }
}
