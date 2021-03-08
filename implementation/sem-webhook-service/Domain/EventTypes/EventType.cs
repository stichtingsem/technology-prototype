namespace Domain.EventTypes
{
    public sealed class EventType
    {
        public EventType(EventTypeId id, EventTypeName name)
        {
            Id = id;
            Name = name;
        }

        public EventTypeId Id { get; }

        public EventTypeName Name { get; }
    }
}
