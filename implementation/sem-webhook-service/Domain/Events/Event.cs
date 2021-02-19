using System;

namespace Domain.Events
{
    public sealed class Event
    {
        private readonly EventName name;

        public Event(EventId id, EventName name)
        {
            Id = id;
            this.name = name;
        }

        public EventId Id { get; }

        public TResult Convert<TResult>(Func<EventId, EventName, TResult> convert) => convert(Id, name);

        //public bool IsFor(EventId eventId) => id == eventId;
    }
}
