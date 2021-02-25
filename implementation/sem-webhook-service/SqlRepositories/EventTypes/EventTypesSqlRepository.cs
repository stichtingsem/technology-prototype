using Dapper;
using Domain.EventTypes;
using Domain.Generic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SqlRepositories.EventTypes
{
    public sealed class EventTypesSqlRepository : IEventTypesRepository
    {
        private readonly EventTypesSqlConnectionString connectionString;

        public EventTypesSqlRepository(EventTypesSqlConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public Maybe<EventType> Get(EventTypeId eventTypeId)
        {
            var sqlParams = new { Id = eventTypeId.Value };
            var selectSql = $"select * from dbo.[EventTypes] where Id = @{nameof(sqlParams.Id)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var results = connection.Query<EventTypeOutput>(selectSql, sqlParams);

                if (!results.Any())
                    return Maybe.None;

                var eventTypeOutput = results.Single();
                return new EventType(eventTypeOutput.Id, eventTypeOutput.Name);
            }
        }

        public IEnumerable<EventType> GetAll()
        {
            var selectSql = $"select * from dbo.[EventTypes]";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<EventTypeOutput>(selectSql).Select(e => new EventType(e.Id, e.Name)).ToList();
            }
        }
    }
}
