using Domain.Generic;
using System.Collections.Generic;

namespace Domain.Events
{
    public interface IEventsRepository
    {
        IEnumerable<Event> GetAll();
        
        Maybe<Event> Get(EventId eventId);
    }
}
