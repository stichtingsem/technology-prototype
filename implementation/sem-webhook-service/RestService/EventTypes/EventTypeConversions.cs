using Domain.EventTypes;
using System.Collections.Generic;
using System.Linq;

namespace RestService.EventTypes
{
    public static class EventTypeConversions
    {
        public static EventTypeOutput ToOutput(this EventType ev) => 
            new EventTypeOutput(ev.Id, ev.Name);

        public static IEnumerable<EventTypeOutput> ToOutput(this IEnumerable<EventType> eventTypes) =>
            eventTypes.Select(ev => ev.ToOutput()).ToList();
    }
}
