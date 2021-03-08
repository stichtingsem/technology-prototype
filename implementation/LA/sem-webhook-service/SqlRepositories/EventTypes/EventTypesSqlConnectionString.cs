using Domain.Generic;

namespace SqlRepositories.EventTypes
{
    public sealed class EventTypesSqlConnectionString : ValueObject<string>
    {
        public EventTypesSqlConnectionString(string value) : base(value) { }

        public static implicit operator EventTypesSqlConnectionString(string value) => new EventTypesSqlConnectionString(value);
    }
}