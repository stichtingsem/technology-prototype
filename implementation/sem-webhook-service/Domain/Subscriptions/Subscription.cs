using Domain.Events;
using Domain.Schools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Subscriptions
{
    public sealed class Subscription : IEquatable<Subscription>
    {
        public static Subscription Create(Guid schoolId, IEnumerable<Guid> eventIds, string postbackUrl, string secret) =>
            Create(SubscriptionId.Create(), schoolId, eventIds, postbackUrl, secret);

        public static Subscription Create(Guid subscriptionId, Guid schoolId, IEnumerable<Guid> eventIds, string postbackUrl, string secret) =>
            new Subscription(subscriptionId, schoolId, eventIds.Select(eventId => new EventId(eventId)), postbackUrl, secret);

        private readonly IEnumerable<EventId> eventIds;
        private readonly SubscriptionId subscriptionId;
        private readonly SchoolId schoolId;
        private readonly PostbackUrl postbackUrl;
        private readonly Secret secret;

        public Subscription(SubscriptionId subscriptionId, SchoolId schoolId, IEnumerable<EventId> eventIds, PostbackUrl postbackUrl, Secret secret)
        {
            this.eventIds = eventIds;
            this.subscriptionId = subscriptionId;
            this.schoolId = schoolId;
            this.postbackUrl = postbackUrl;
            this.secret = secret;
        }

        public Result Convert<Result>(Func<SubscriptionId, IEnumerable<EventId>, PostbackUrl, Secret, Result> convert) => convert(subscriptionId, eventIds, postbackUrl, secret);

        public bool Equals(Subscription other) => other.IsFor(subscriptionId);

        public override bool Equals(object obj) => Equals(obj as Subscription);

        public override int GetHashCode() => subscriptionId.GetHashCode();

        public static bool operator ==(Subscription left, Subscription right) => left.Equals(right);

        public static bool operator !=(Subscription left, Subscription right) => !(left == right);

        public bool IsFor(SubscriptionId subscriptionId, SchoolId schoolId) => IsFor(subscriptionId) && IsFor(schoolId);

        public bool IsFor(SchoolId schoolId) => this.schoolId == schoolId;

        public bool IsFor(SubscriptionId subscriptionId) => this.subscriptionId == subscriptionId;
    }
}
