using Domain.Generic;

namespace SqlRepositories.Events
{
    public sealed class EventsSqlConnectionString : ValueObject<string>
    {
        public EventsSqlConnectionString(string value) : base(value) { }

        public static implicit operator EventsSqlConnectionString(string value) => new EventsSqlConnectionString(value);
    }
}