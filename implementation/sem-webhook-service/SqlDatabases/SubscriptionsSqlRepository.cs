using Domain.Schools;
using Domain.Subscriptions;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositories
{
    public sealed class SubscriptionsSqlRepository : ISubscriptionsRepository
    {
        static readonly List<Subscription> subscriptions = new List<Subscription>();

        public void Add(Subscription subscription)
        {
            subscriptions.Add(subscription);
        }

        public void Delete(SubscriptionId subscriptionId, SchoolId schoolId)
        {
            subscriptions.RemoveAll(subscription => subscription.IsFor(subscriptionId, schoolId));
        }

        public IEnumerable<Subscription> GetAll(SchoolId schoolId)
        {
            return subscriptions.Where(subscription => subscription.IsFor(schoolId));
        }

        public Subscription Get(SubscriptionId subscriptionId, SchoolId schoolId)
        {
            return subscriptions.SingleOrDefault(subscription => subscription.IsFor(subscriptionId, schoolId));
        }

        public void Update(Subscription subscription, SchoolId schoolId)
        {
            var index = subscriptions.FindIndex(s => s == subscription && s.IsFor(schoolId));

            if (index >= 0)
                subscriptions[index] = subscription;
        }
    }
}
