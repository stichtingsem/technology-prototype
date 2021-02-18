using System;

namespace SqlRepositories.Events
{
    internal sealed record EventOutput(Guid Id, string Name);
}
