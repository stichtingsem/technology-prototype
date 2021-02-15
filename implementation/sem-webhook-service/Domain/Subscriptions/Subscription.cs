using Domain.Events;
using Domain.Schools;
using System;

namespace Domain.Subscriptions
{
    public sealed class Subscription : IEquatable<Subscription>
    {
        private readonly EventId eventId;
        private readonly SchoolId schoolId;
        private readonly PostbackUrl postbackUrl;
        private readonly Secret secret;

        public Subscription(SchoolId schoolId, EventId eventId, PostbackUrl postbackUrl, Secret secret)
        {
            this.eventId = eventId;
            this.schoolId = schoolId;
            this.postbackUrl = postbackUrl;
            this.secret = secret;
        }

        public Result Convert<Result>(Func<EventId, PostbackUrl, Secret, Result> convert) => convert(eventId, postbackUrl, secret);

        public bool Equals(Subscription other) => other.IsFor(schoolId, eventId);

        public override bool Equals(object obj) => Equals(obj as Subscription);

        public override int GetHashCode() => schoolId.GetHashCode() ^ eventId.GetHashCode();

        public static bool operator ==(Subscription left, Subscription right) => left.Equals(right);

        public static bool operator !=(Subscription left, Subscription right) => !(left == right);

        public bool IsFor(SchoolId schoolId, EventId eventId) => IsFor(schoolId) && this.eventId == eventId;

        public bool IsFor(SchoolId schoolId) => this.schoolId == schoolId;

        public bool IsFor(Event theEvent) => theEvent.IsFor(eventId);
    }
}
