using Domain.Events;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Events
{
    public static class EventExtensions
    {
        public static EventOutput ToOutput(this Event anEvent) => 
            anEvent.Convert((eventId, eventName) => new EventOutput(eventId, eventName));

        public static IEnumerable<EventOutput> ToOutput(this IEnumerable<Event> events) =>
            events.Select(anEvent => anEvent.ToOutput()).ToList();
    }
}
