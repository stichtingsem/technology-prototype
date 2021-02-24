using Domain.Generic;
using System;

namespace Domain.EventTypes
{
    public sealed class EventTypeId : ValueObject<Guid>
    {
        public EventTypeId(Guid value) : base(value) { }

        public static implicit operator EventTypeId(Guid value) => new EventTypeId(value);
    }
}
