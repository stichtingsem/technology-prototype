using Abp.Webhooks;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositories.WebhookSubscriptions
{
    public sealed class WebhookSubscriptionsSqlStore : IWebhookSubscriptionsStore
    {
        private readonly WebhooksSqlConnection sqlConnection;

        public WebhookSubscriptionsSqlStore(WebhooksSqlConnection sqlConnection) => 
            this.sqlConnection = sqlConnection;

        public void Delete(Guid id)
        {
            var sqlParams = new { Id = id };
            var deleteSql =
                @$"delete from dbo.WebhookSubscriptionInfo where Id = @{nameof(sqlParams.Id)}";

            using (var connection = sqlConnection.Create())
            {
                connection.Execute(deleteSql, sqlParams);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var sqlParams = new { Id = id };
            var deleteSql =
                @$"delete from dbo.WebhookSubscriptionInfo where Id = @{nameof(sqlParams.Id)}";

            using (var connection = sqlConnection.Create())
            {
                await connection.ExecuteAsync(deleteSql, sqlParams);
            }
        }

        public WebhookSubscriptionInfo Get(Guid id)
        {
            var sqlParams = new { Id = id };
            var sql = $"select * from dbo.WebhookSubscriptionInfo where Id = @{nameof(sqlParams.Id)}";

            using (var connection = sqlConnection.Create())
            {
                var result = connection.Query<WebhookSubscriptionInfo>(sql, sqlParams);
                
                if (!result.Any())
                    return null; // I'm so sorry :(
                
                return result.Single();
            }
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(int? tenantId)
        {
            var sqlParams = new { TenantId = tenantId };
            var sql =
                @$"select *
                   from dbo.WebhookSubscriptionInfo 
                   where TenantId = @{nameof(sqlParams.TenantId)}";

            using (var connection = sqlConnection.Create())
            {
                return connection.Query<WebhookSubscriptionInfo>(sql, sqlParams).ToList();
            }
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(int? tenantId, string webhookName) => 
            GetAllSubscriptions(tenantId).Where(s => s.IsSubscribed(webhookName)).ToList();

        public async Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(int? tenantId)
        {
            var sqlParams = new { TenantId = tenantId };
            var sql =
                @$"select *
                   from dbo.WebhookSubscriptionInfo 
                   where TenantId = @{nameof(sqlParams.TenantId)}";

            using (var connection = sqlConnection.Create())
            {
                return (await connection.QueryAsync<WebhookSubscriptionInfo>(sql, sqlParams)).ToList();
            }
        }

        public async Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(int? tenantId, string webhookName) =>
            (await GetAllSubscriptionsAsync(tenantId)).Where(s => s.IsSubscribed(webhookName)).ToList();

        public async Task<WebhookSubscriptionInfo> GetAsync(Guid id)
        {
            var sqlParams = new { Id = id };
            var sql = $"select * from dbo.WebhookSubscriptionInfo where Id = @{nameof(sqlParams.Id)}";

            using (var connection = sqlConnection.Create())
            {
                return (await connection.QueryAsync<WebhookSubscriptionInfo>(sql, sqlParams)).SingleOrDefault();
            }
        }

        public void Insert(WebhookSubscriptionInfo webhookSubscription)
        {
            var insertWebhookSubscriptionsSql = 
                $@"insert into dbo.WebhookSubscriptionInfo (Id, TenantId, Secret, WebhookUri, Webhooks, Headers, IsActive, CreationTime)
                   values 
                   (
                      @{nameof(webhookSubscription.Id)}, @{nameof(webhookSubscription.TenantId)}, 
                      @{nameof(webhookSubscription.Secret)}, @{nameof(webhookSubscription.WebhookUri)}, 
                      @{nameof(webhookSubscription.Webhooks)}, @{nameof(webhookSubscription.Headers)}, 
                      @{nameof(webhookSubscription.IsActive)}, @{nameof(webhookSubscription.CreationTime)}
                   )";

            using (var connection = sqlConnection.Create())
            {
                connection.Execute(insertWebhookSubscriptionsSql, webhookSubscription);
            }
        }

        public async Task InsertAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            var insertWebhookSubscriptionsSql =
                $@"insert into dbo.WebhookSubscriptionInfo (Id, TenantId, Secret, WebhookUri, Webhooks, Headers, IsActive, CreationTime)
                   values 
                   (
                      @{nameof(webhookSubscription.Id)}, @{nameof(webhookSubscription.TenantId)}, 
                      @{nameof(webhookSubscription.Secret)}, @{nameof(webhookSubscription.WebhookUri)}, 
                      @{nameof(webhookSubscription.Webhooks)}, @{nameof(webhookSubscription.Headers)}, 
                      @{nameof(webhookSubscription.IsActive)}, @{nameof(webhookSubscription.CreationTime)}
                   )";

            using (var connection = sqlConnection.Create())
            {
                await connection.ExecuteAsync(insertWebhookSubscriptionsSql, webhookSubscription);
            }
        }

        public bool IsSubscribed(int? tenantId, string webhookName) =>
            GetAllSubscriptions(tenantId, webhookName).Any();

        public async Task<bool> IsSubscribedAsync(int? tenantId, string webhookName) => 
            (await GetAllSubscriptionsAsync(tenantId, webhookName)).Any();

        public void Update(WebhookSubscriptionInfo webhookSubscription)
        {
            var sql =
                $@"update dbo.WebhookSubscriptionInfo 
                   set TenantId = @{nameof(webhookSubscription.TenantId)}, Secret = @{nameof(webhookSubscription.Secret)}, 
                       WebhookUri = @{nameof(webhookSubscription.TenantId)}, Webhooks = @{nameof(webhookSubscription.TenantId)}, 
                       Headers = @{nameof(webhookSubscription.Headers)}, IsActive = @{nameof(webhookSubscription.IsActive)}, 
                       CreationTime = @{nameof(webhookSubscription.CreationTime)}
                   where Id = @Id";

            using (var connection = sqlConnection.Create())
            {
                connection.Execute(sql, webhookSubscription);
            }
        }

        public async Task UpdateAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            var sql =
                $@"update dbo.WebhookSubscriptionInfo 
                   set TenantId = @{nameof(webhookSubscription.TenantId)}, Secret = @{nameof(webhookSubscription.Secret)}, 
                       WebhookUri = @{nameof(webhookSubscription.TenantId)}, Webhooks = @{nameof(webhookSubscription.TenantId)}, 
                       Headers = @{nameof(webhookSubscription.Headers)}, IsActive = @{nameof(webhookSubscription.IsActive)}, 
                       CreationTime = @{nameof(webhookSubscription.CreationTime)}
                   where Id = @Id";

            using (var connection = sqlConnection.Create())
            {
                await connection.ExecuteAsync(sql, webhookSubscription);
            }
        }
    }
}
