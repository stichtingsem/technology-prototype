using Domain.Events;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Events
{
    public static class EventConversions
    {
        public static EventOutput ToOutput(this Event ev) => 
            new EventOutput(ev.Id, ev.Name);

        public static IEnumerable<EventOutput> ToOutput(this IEnumerable<Event> events) =>
            events.Select(anEvent => anEvent.ToOutput()).ToList();
    }
}
