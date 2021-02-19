namespace Domain.Events
{
    public sealed class Event
    {
        public Event(EventId id, EventName name)
        {
            Id = id;
            Name = name;
        }

        public EventId Id { get; }

        public EventName Name { get; }
    }
}
