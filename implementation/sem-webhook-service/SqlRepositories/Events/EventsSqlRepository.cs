using Dapper;
using Domain.Events;
using Domain.Generic;
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

        public Maybe<Event> Get(EventId eventId)
        {
            var sqlParams = new { Id = eventId.Value };
            var selectSql = $"select * from dbo.[Events] where Id = @{nameof(sqlParams.Id)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var results = connection.Query<EventOutput>(selectSql, sqlParams);

                if (!results.Any())
                    return Maybe.None;

                var eventOutput = results.Single();
                return new Event(eventOutput.Id, eventOutput.Name);
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
