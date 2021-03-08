using Dapper;
using Domain.EventTypes;
using Domain.Generic;
using Domain.Tenants;
using Domain.Webhooks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SqlRepositories.WebhookSubscriptions
{
    public sealed class WebhooksSqlRepository : IWebhooksRepository
    {
        private readonly WebhooksSqlConnectionString connectionString;

        public WebhooksSqlRepository(WebhooksSqlConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(Webhook webhook)
        {
            var webhookInput = webhook.ToInput();

            var insertWebhookSql =
                $"insert into ObsoleteWebhooks values (@{nameof(WebhookInput.Id)}, @{nameof(WebhookInput.TenantId)}, @{nameof(WebhookInput.PostbackUrl)}, @{nameof(WebhookInput.Secret)})";

            var deleteSubscriptionsSql =
                $"delete from Subscriptions where WebhookId = @{nameof(WebhookInput.Id)}";

            var subscriptionsInput = webhookInput.EventTypeIds.Select(eventTypeId => new SubscriptionInput(webhookInput.Id, eventTypeId));

            var insertSubscriptionsSql =
                $"insert into Subscriptions values (@{nameof(SubscriptionInput.WebhookId)}, @{nameof(SubscriptionInput.EventTypeId)})";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(insertWebhookSql, webhookInput, transaction);
                    connection.Execute(deleteSubscriptionsSql, webhookInput, transaction);
                    connection.Execute(insertSubscriptionsSql, subscriptionsInput, transaction);

                    // Surely, there must be a more efficient way of updating the subscriptions?

                    transaction.Commit();
                }
            }
        }

        public void Delete(WebhookId webhookId, TenantId tenantId)
        {
            var deleteSubscriptionsParams = new { WebhookId = webhookId.Value };
            var deleteSubscriptionsSql =
                @$"delete from Subscriptions
                   where WebhookId = @{nameof(deleteSubscriptionsParams.WebhookId)}";

            var deleteWebhooksParams = new { WebhookId = webhookId.Value, TenantId = tenantId.Value };
            var deleteWebhooksSql = 
                @$"delete from ObsoleteWebhooks
                   where Id = @{nameof(deleteWebhooksParams.WebhookId)} and TenantId = @{nameof(deleteWebhooksParams.TenantId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(deleteSubscriptionsSql, deleteSubscriptionsParams);
                connection.Execute(deleteWebhooksSql, deleteWebhooksParams);
            }
        }

        public Maybe<Webhook> Get(WebhookId webhookId, TenantId tenantId)
        {
            var selectParams = new { WebhookId = webhookId.Value, TenantId = tenantId.Value };

            var selectSql =
                @$"select wh.Id, wh.TenantId, wh.PostbackUrl, wh.Secret, sub.EventTypeId
                   from dbo.ObsoleteWebhooks wh
                   inner join dbo.[Subscriptions] sub on sub.WebhookId = wh.Id
                   where wh.Id = @{nameof(selectParams.WebhookId)} and wh.TenantId = @{nameof(selectParams.TenantId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var webhookOutputs = connection.Query<WebhookOutput>(selectSql, selectParams).ToList();

                if (!webhookOutputs.Any())
                    return Maybe.None;
                
                var webhookOutput = webhookOutputs.First();
                var eventTypeIds = webhookOutputs.Select(output => new EventTypeId(output.EventTypeId)).ToList();

                return new Webhook(webhookOutput.Id, webhookOutput.TenantId, eventTypeIds, webhookOutput.PostbackUrl, webhookOutput.Secret);
            }
        }

        public IEnumerable<Webhook> GetAll(TenantId tenantId)
        {
            var selectParams = new { TenantId = tenantId.Value };
            var selectSql =
                @$"select wh.Id, wh.TenantId, wh.PostbackUrl, wh.Secret, sub.EventTypeId
                   from dbo.ObsoleteWebhooks wh
                   inner join dbo.[Subscriptions] sub on sub.WebhookId = wh.Id
                   where wh.TenantId = @{nameof(selectParams.TenantId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var webhookOutputs = connection.Query<WebhookOutput>(selectSql, selectParams).ToList();

                return Convert(webhookOutputs);
            }
        }

        private IEnumerable<Webhook> Convert(List<WebhookOutput> webhookOutputs)
        {
            // Can Dapper do this grouping for us??
            var webhookGroupings = webhookOutputs.GroupBy(webhook => webhook.Id);
            foreach (var webhookGrouping in webhookGroupings)
            {
                var webhookOutput = webhookGrouping.First();
                var eventTypeIds = webhookGrouping.Select(o => new EventTypeId(o.EventTypeId)).ToList();

                yield return new Webhook(webhookOutput.Id, webhookOutput.TenantId, eventTypeIds, webhookOutput.PostbackUrl, webhookOutput.Secret);
            }
        }

        public void Update(Webhook webhook)
        {
            var webhookInput = webhook.ToInput();

            var updateWebhookSql =
                @$"update ObsoleteWebhooks 
                   set PostbackUrl = @{nameof(WebhookInput.PostbackUrl)}, Secret = @{nameof(WebhookInput.Secret)}
                   where Id = @{nameof(WebhookInput.Id)} and TenantId = @{nameof(WebhookInput.TenantId)}"; 

            var deleteSubscriptionsSql =
                $"delete from Subscriptions where WebhookId = @{nameof(WebhookInput.Id)}";

            var subscriptionsInput = webhookInput.EventTypeIds.Select(eventTypeId => new SubscriptionInput(webhookInput.Id, eventTypeId));

            var insertSubscriptionsSql =
                $"insert into Subscriptions values (@{nameof(SubscriptionInput.WebhookId)}, @{nameof(SubscriptionInput.EventTypeId)})";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(updateWebhookSql, webhookInput, transaction);
                    connection.Execute(deleteSubscriptionsSql, webhookInput, transaction);
                    connection.Execute(insertSubscriptionsSql, subscriptionsInput, transaction);

                    // Surely, there must be a more efficient way of updating the subscriptions?

                    transaction.Commit();
                }
            }
        }
    }
}
