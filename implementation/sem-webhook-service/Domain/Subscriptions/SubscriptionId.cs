using Domain.Generic;
using System;

namespace Domain.Subscriptions
{
    public class SubscriptionId : ValueObject<Guid>
    {
        public static SubscriptionId Create() => new SubscriptionId(Guid.NewGuid());

        public SubscriptionId(Guid value) : base(value) { }

        public static implicit operator SubscriptionId(Guid value) => new SubscriptionId(value);
    }
}
