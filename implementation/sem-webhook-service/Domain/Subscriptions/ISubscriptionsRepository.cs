using Domain.Schools;
using System.Collections.Generic;

namespace Domain.Subscriptions
{
    public interface ISubscriptionsRepository
    {
        IEnumerable<Subscription> GetAll(SchoolId schoolId);

        Subscription Get(SubscriptionId subscriptionId, SchoolId schoolId);

        void Add(Subscription subscription);

        void Update(Subscription subscription, SchoolId schoolId);

        void Delete(SubscriptionId subscriptionId, SchoolId schoolId);
    }
}
