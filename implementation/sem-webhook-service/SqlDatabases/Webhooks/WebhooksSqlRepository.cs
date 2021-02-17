using Dapper;
using Domain.Events;
using Domain.Generic;
using Domain.Schools;
using Domain.Webhooks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SqlRepositories.Webhooks
{
    public sealed partial class WebhooksSqlRepository : IWebhooksRepository
    {
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Webhooks;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Add(Webhook webhook)
        {
            var webhookInput = webhook.Convert((id, schoolId, eventIds, postbackUrl, secret) =>
                new WebhookInput(id, schoolId, eventIds.ToValues(), postbackUrl, secret));

            var insertWebhookSql =
                $"insert into Webhooks values (@{nameof(WebhookInput.Id)}, @{nameof(WebhookInput.SchoolId)}, @{nameof(WebhookInput.PostbackUrl)}, @{nameof(WebhookInput.Secret)})";

            var deleteSubscriptionsSql =
                $"delete from Subscriptions where WebhookId = @{nameof(WebhookInput.Id)}";

            var subscriptionsInput = webhookInput.EventIds.Select(eventId => new SubscriptionInput(webhookInput.Id, eventId));

            var insertSubscriptionsSql =
                $"insert into Subscriptions values (@{nameof(SubscriptionInput.WebhookId)}, @{nameof(SubscriptionInput.EventId)})";

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

        public void Delete(WebhookId webhookId, SchoolId schoolId)
        {
            var deleteSubscriptionsParams = new { WebhookId = webhookId.Value };
            var deleteSubscriptionsSql =
                @$"delete from Subscriptions
                   where WebhookId = @{nameof(deleteSubscriptionsParams.WebhookId)}";

            var deleteWebhooksParams = new { WebhookId = webhookId.Value, SchoolId = schoolId.Value };
            var deleteWebhooksSql = 
                @$"delete from Webhooks
                   where Id = @{nameof(deleteWebhooksParams.WebhookId)} and SchoolId = @{nameof(deleteWebhooksParams.SchoolId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(deleteSubscriptionsSql, deleteSubscriptionsParams);
                connection.Execute(deleteWebhooksSql, deleteWebhooksParams);
            }
        }

        public Webhook Get(WebhookId webhookId, SchoolId schoolId)
        {
            var selectParams = new { WebhookId = webhookId.Value, SchoolId = schoolId.Value };

            var selectSql =
                @$"select wh.Id, wh.SchoolId, wh.PostbackUrl, wh.Secret, sub.EventId
                   from dbo.Webhooks wh
                   inner join dbo.[Subscriptions] sub on sub.WebhookId = wh.Id
                   where wh.Id = @{nameof(selectParams.WebhookId)} and wh.SchoolId = @{nameof(selectParams.SchoolId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var webhookOutputs = connection.Query<WebhookOutput>(selectSql, selectParams).ToList();

                if (!webhookOutputs.Any())
                    return null;
                
                var webhookOutput = webhookOutputs.First();
                var eventIds = webhookOutputs.Select(o => new EventId(o.EventId)).ToList();

                return new Webhook(webhookOutput.Id, webhookOutput.SchoolId, eventIds, webhookOutput.PostbackUrl, webhookOutput.Secret);
            }
        }

        public IEnumerable<Webhook> GetAll(SchoolId schoolId)
        {
            var selectParams = new { SchoolId = schoolId.Value };
            var selectSql =
                @$"select wh.Id, wh.SchoolId, wh.PostbackUrl, wh.Secret, sub.EventId
                   from dbo.Webhooks wh
                   inner join dbo.[Subscriptions] sub on sub.WebhookId = wh.Id
                   where wh.SchoolId = @{nameof(selectParams.SchoolId)}";

            using (var connection = new SqlConnection(connectionString))
            {
                var webhookOutputs = connection.Query<WebhookOutput>(selectSql, selectParams).ToList();

                return Convert(webhookOutputs);
            }
        }

        private IEnumerable<Webhook> Convert(List<WebhookOutput> webhookOutputs)
        {
            var webhookGroupings = webhookOutputs.GroupBy(webhook => webhook.Id);
            foreach (var webhookGrouping in webhookGroupings)
            {
                var webhookOutput = webhookGrouping.First();
                var eventIds = webhookGrouping.Select(o => new EventId(o.EventId)).ToList();

                yield return new Webhook(webhookOutput.Id, webhookOutput.SchoolId, eventIds, webhookOutput.PostbackUrl, webhookOutput.Secret);
            }
        }

        public void Update(Webhook webhook)
        {
            var webhookInput = webhook.Convert((id, schoolId, eventIds, postbackUrl, secret) =>
                new WebhookInput(id, schoolId, eventIds.ToValues(), postbackUrl, secret));

            var updateWebhookSql =
                @$"update Webhooks 
                   set PostbackUrl = @{nameof(WebhookInput.PostbackUrl)}, Secret = @{nameof(WebhookInput.Secret)}
                   where Id = @{nameof(WebhookInput.Id)} and SchoolId = @{nameof(WebhookInput.SchoolId)}"; 

            var deleteSubscriptionsSql =
                $"delete from Subscriptions where WebhookId = @{nameof(WebhookInput.Id)}";

            var subscriptionsInput = webhookInput.EventIds.Select(eventId => new SubscriptionInput(webhookInput.Id, eventId));

            var insertSubscriptionsSql =
                $"insert into Subscriptions values (@{nameof(SubscriptionInput.WebhookId)}, @{nameof(SubscriptionInput.EventId)})";

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
