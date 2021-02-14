using System;
using System.Collections.Generic;

namespace Domain.Events
{
    public interface IEventsRepository
    {
        IEnumerable<Event> GetAll();
        
        Event Get(Guid eventId);
    }
}
