using Domain.Schools;
using RestService.Subscriptions;
using System.Collections.Generic;

namespace Domain.Subscriptions
{
    public interface ISubscriptionsRepository
    {
        IEnumerable<Subscription> Get(ISchool schoolAdmin);
    }
}
