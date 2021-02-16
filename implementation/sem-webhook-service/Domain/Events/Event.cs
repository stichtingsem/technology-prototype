using System;

namespace Domain.Events
{
    public sealed class Event
    {
        private readonly EventId id;
        private readonly EventName name;

        public Event(EventId id, EventName name)
        {
            this.id = id;
            this.name = name;
        }

        public TResult Convert<TResult>(Func<EventId, EventName, TResult> convert) => convert(id, name);

        public bool IsFor(EventId eventId) => id == eventId;
    }
}
