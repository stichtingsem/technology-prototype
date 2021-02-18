using Dapper;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SqlRepositories.Events
{
    public sealed class EventsSqlRepository : IEventsRepository
    {
        private readonly EventsSqlConnectionString connectionString;

        public EventsSqlRepository(EventsSqlConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public Event Get(Guid eventId)
        {
            var selectSql = $"select * from dbo.[Events] where Id = @{nameof(eventId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var eventDto = connection.QuerySingle<EventOutput>(selectSql, eventId);

                return new Event(eventDto.Id, eventDto.Name);
            }
        }

        public IEnumerable<Event> GetAll()
        {
            var selectSql = $"select * from dbo.[Events]";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<EventOutput>(selectSql).Select(e => new Event(e.Id, e.Name)).ToList();
            }
        }
    }
}
