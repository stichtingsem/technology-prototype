using System;

namespace Domain.Events
{
    public sealed record Event(Guid Id, string Name);
}
