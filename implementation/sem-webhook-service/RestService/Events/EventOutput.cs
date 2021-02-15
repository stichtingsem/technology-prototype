using System;

namespace RestService.Events
{
    public sealed record EventOutput(Guid Id, string Name);
}
