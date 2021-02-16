using Domain.Generic;

namespace Domain.Events
{
    public sealed class EventName : ValueObject<string>
    {
        public EventName(string value) : base(value) { }

        public static implicit operator EventName(string value) => new EventName(value);
    }
}
