using Domain.Schools;
using RestService.Subscriptions;
using System;
using System.Collections.Generic;

namespace Domain.Subscriptions
{
    public interface ISubscriptionsRepository
    {
        IEnumerable<Subscription> Get(ISchool school);

        void Add(Subscription subscription);

        Subscription Get(ISchool school, Guid eventId);
        void Delete(ISchool school, Guid eventId);
    }
}
