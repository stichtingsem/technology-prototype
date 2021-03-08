using Domain.Generic;
using System.Collections.Generic;

namespace Domain.EventTypes
{
    public interface IEventTypesRepository
    {
        IEnumerable<EventType> GetAll();
        
        Maybe<EventType> Get(EventTypeId eventId);
    }
}
