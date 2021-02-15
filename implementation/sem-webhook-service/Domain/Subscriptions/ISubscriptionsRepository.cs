using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using System.Collections.Generic;

namespace Domain.Subscriptions
{
    public interface ISubscriptionsRepository
    {
        IEnumerable<Subscription> GetAll(SchoolId schoolId);

        Subscription Get(SchoolId schoolId, EventId eventId);

        void AddOrUpdate(Subscription subscription);

        void Delete(SchoolId schoolId, EventId eventId);
    }
}
