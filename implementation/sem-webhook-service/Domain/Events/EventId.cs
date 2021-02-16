﻿using Domain.Generic;
using System;

namespace Domain.Events
{
    public sealed class EventId : ValueObject<Guid>
    {
        public EventId(Guid value) : base(value) { }

        public static implicit operator EventId(Guid value) => new EventId(value);
    }
}