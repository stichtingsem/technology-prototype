using System.Collections.Generic;

namespace Domain.Events
{
    public interface IEventsRepository
    {
        IEnumerable<Event> Get();
    }
}
