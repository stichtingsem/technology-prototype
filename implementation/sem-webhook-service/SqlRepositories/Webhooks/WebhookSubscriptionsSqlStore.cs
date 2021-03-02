using Abp.Webhooks;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SqlRepositories.Webhooks
{
    public sealed class WebhookSubscriptionsSqlStore : IWebhookSubscriptionsStore
    {
        private readonly WebhooksSqlConnectionString connectionString;

        public WebhookSubscriptionsSqlStore(WebhooksSqlConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public WebhookSubscriptionInfo Get(Guid id)
        {
            var sqlParams = new { Id = id };
            var sql = $"select * from dbo.WebhookSubscriptionInfo where Id = @{nameof(sqlParams.Id)}";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<WebhookSubscriptionInfo>(sql, sqlParams).Single();
            }
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(int? tenantId)
        {
            throw new NotImplementedException();
        }

        public List<WebhookSubscriptionInfo> GetAllSubscriptions(int? tenantId, string webhookName)
        {
            throw new NotImplementedException();
        }

        public Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(int? tenantId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WebhookSubscriptionInfo>> GetAllSubscriptionsAsync(int? tenantId, string webhookName)
        {
            throw new NotImplementedException();
        }

        public Task<WebhookSubscriptionInfo> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(WebhookSubscriptionInfo webhookSubscription)
        {
            var insertWebhookSubscriptions = 
                @"insert into dbo.WebhookSubscriptionInfo (Id, TenantId, Secret, WebhookUri, Webhooks, IsActive, CreationTime)
                  values (@Id, @TenantId, @Secret, @WebhookUri, @Webhooks, @IsActive, @CreationTime)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(insertWebhookSubscriptions, webhookSubscription);
            }
        }

        public Task InsertAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            throw new NotImplementedException();
        }

        public bool IsSubscribed(int? tenantId, string webhookName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsSubscribedAsync(int? tenantId, string webhookName)
        {
            throw new NotImplementedException();
        }

        public void Update(WebhookSubscriptionInfo webhookSubscription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WebhookSubscriptionInfo webhookSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
