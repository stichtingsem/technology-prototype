using Domain.Generic;

namespace Domain.EventTypes
{
    public sealed class EventTypeName : ValueObject<string>
    {
        public EventTypeName(string value) : base(value) { }

        public static implicit operator EventTypeName(string value) => new EventTypeName(value);
    }
}
