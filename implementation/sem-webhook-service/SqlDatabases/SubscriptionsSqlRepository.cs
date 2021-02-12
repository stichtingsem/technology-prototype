using Domain.Schools;
using Domain.Subscriptions;
using RestService.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlDatabases
{
    public sealed class SubscriptionsSqlRepository : ISubscriptionsRepository
    {
        static readonly List<Subscription> subscriptions = new List<Subscription>();

        public void Add(Subscription subscriptionToAdd)
        {
            subscriptions.RemoveAll(subscription => subscription.SchoolId == subscriptionToAdd.SchoolId && subscription.EventId == subscriptionToAdd.EventId);
            subscriptions.Add(subscriptionToAdd);
        }

        public void Delete(ISchool school, Guid eventId)
        {
            subscriptions.RemoveAll(subscription => subscription.SchoolId == school.Id && subscription.EventId == eventId);
        }

        public IEnumerable<Subscription> Get(ISchool school)
        {
            return subscriptions.Where(subscription => subscription.SchoolId == school.Id);
        }

        public Subscription Get(ISchool school, Guid eventId)
        {
            return subscriptions.SingleOrDefault(subscription => subscription.SchoolId == school.Id && subscription.EventId == eventId);
        }
    }
}
