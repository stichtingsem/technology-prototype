using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositories
{
    public sealed class SubscriptionsSqlRepository : ISubscriptionsRepository
    {
        static readonly List<Subscription> subscriptions = new List<Subscription>();

        public void AddOrUpdate(Subscription subscriptionToMerge)
        {
            subscriptions.RemoveAll(subscription => subscription == subscriptionToMerge);
            subscriptions.Add(subscriptionToMerge);
        }

        public void Delete(SchoolId schoolId, EventId eventId)
        {
            subscriptions.RemoveAll(subscription => subscription.IsFor(schoolId, eventId));
        }

        public IEnumerable<Subscription> GetAll(SchoolId schoolId)
        {
            return subscriptions.Where(subscription => subscription.IsFor(schoolId));
        }

        public Subscription Get(SchoolId schoolId, EventId eventId)
        {
            return subscriptions.SingleOrDefault(subscription => subscription.IsFor(schoolId, eventId));
        }
    }
}
