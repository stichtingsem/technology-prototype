using Abp.Webhooks;
using Dapper;
using SqlRepositories.WebhookSubscriptions;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositories.WebhookEvents
{
    public sealed class WebhookEventsSqlStore : IWebhookEventStore
    {
        private readonly WebhooksSqlConnection sqlConnection;

        public WebhookEventsSqlStore(WebhooksSqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public WebhookEvent Get(int? tenantId, Guid id)
        {
            var sqlParams = new { TenantId = tenantId, Id = id };
            var sql =
                $@"select * from dbo.WebhookEvent
                   where Id = @{nameof(sqlParams.Id)} and TenantId = {nameof(sqlParams.TenantId)}";

            using (var connection = sqlConnection.Create())
            {
                return connection.Query<WebhookEvent>(sql, sqlParams).SingleOrDefault();
            }
        }

        public async Task<WebhookEvent> GetAsync(int? tenantId, Guid id)
        {
            var sqlParams = new { TenantId = tenantId, Id = id };
            var sql =
                $@"select * from dbo.WebhookEvent
                   where Id = @{nameof(sqlParams.Id)} and TenantId = {nameof(sqlParams.TenantId)}";

            using (var connection = sqlConnection.Create())
            {
                return (await connection.QueryAsync<WebhookEvent>(sql, sqlParams)).SingleOrDefault();
            }
        }

        public Guid InsertAndGetId(WebhookEvent webhookEvent)
        {
            var sql =
                $@"insert into dbo.WebhookEvent (Id, TenantId, CreationTime, DeletionTime, IsDeleted, Data, WebhookName)
                   values 
                   (
                      @{nameof(webhookEvent.Id)}, @{nameof(webhookEvent.TenantId)}, 
                      @{nameof(webhookEvent.CreationTime)}, @{nameof(webhookEvent.DeletionTime)}, 
                      @{nameof(webhookEvent.IsDeleted)}, @{nameof(webhookEvent.Data)}, 
                      @{nameof(webhookEvent.WebhookName)}
                   )";

            using (var connection = sqlConnection.Create())
            {
                connection.Execute(sql, webhookEvent);
            }

            return webhookEvent.Id;
        }

        public async Task<Guid> InsertAndGetIdAsync(WebhookEvent webhookEvent)
        {
            var sql =
                $@"insert into dbo.WebhookEvent (Id, TenantId, CreationTime, DeletionTime, IsDeleted, Data, WebhookName)
                   values 
                   (
                      @{nameof(webhookEvent.Id)}, @{nameof(webhookEvent.TenantId)}, 
                      @{nameof(webhookEvent.CreationTime)}, @{nameof(webhookEvent.DeletionTime)}, 
                      @{nameof(webhookEvent.IsDeleted)}, @{nameof(webhookEvent.Data)}, 
                      @{nameof(webhookEvent.WebhookName)}
                   )";

            using (var connection = sqlConnection.Create())
            {
                await connection.ExecuteAsync(sql, webhookEvent);
            }

            return webhookEvent.Id;
        }
    }
}
