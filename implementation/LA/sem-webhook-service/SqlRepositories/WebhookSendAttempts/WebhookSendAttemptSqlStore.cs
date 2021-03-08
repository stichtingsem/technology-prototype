using Abp.Application.Services.Dto;
using Abp.Webhooks;
using Dapper;
using SqlRepositories.WebhookSubscriptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SqlRepositories.WebhookSendAttempts
{
    public class WebhookSendAttemptSqlStore : IWebhookSendAttemptStore
    {
        private readonly WebhooksSqlConnection sqlConnection;

        public WebhookSendAttemptSqlStore(WebhooksSqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public WebhookSendAttempt Get(int? tenantId, Guid id)
        {
            var sqlParams = new { TenantId = tenantId, Id = id };
            var sql =
                $@"select * from WebhookSendAttempt
                  where TenantId = @{nameof(sqlParams.TenantId)} and Id = @{nameof(sqlParams.Id)}";

            using (var connection = sqlConnection.Create())
            {
                return connection.Query<WebhookSendAttempt>(sql, sqlParams).SingleOrDefault();
            }
        }

        public int GetCountSendAttemptsBySubscription(int? tenantId, Guid webhookSubscriptionId)
        {
            var sqlParams = new
            {
                TenantId = tenantId,
                WebhookSubscriptionId = webhookSubscriptionId,
            };

            var sql =
                $@"select count(*)
                   from WebhookSendAttempt
                   where WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     and TenantId = @{nameof(sqlParams.TenantId)}";

            using (var connection = sqlConnection.Create())
            {
                return connection.QueryFirst<int>(sql, sqlParams);
            }
        }

        public async Task<int> GetCountSendAttemptsBySubscriptionAsync(int? tenantId, Guid webhookSubscriptionId)
        {
            var sqlParams = new
            {
                TenantId = tenantId,
                WebhookSubscriptionId = webhookSubscriptionId,
            };

            var sql =
                $@"select count(*)
                   from WebhookSendAttempt
                   where WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     and TenantId = @{nameof(sqlParams.TenantId)}";

            using (var connection = sqlConnection.Create())
            {
                return await connection.QueryFirstAsync<int>(sql, sqlParams);
            }
        }

        public IPagedResult<WebhookSendAttempt> GetAllSendAttemptsBySubscriptionAsPagedList(int? tenantId, Guid subscriptionId, int maxResultCount, int skipCount)
        {
            var totalCount = GetCountSendAttemptsBySubscription(tenantId, subscriptionId);

            var sqlParams = new 
            { 
                TenantId = tenantId, 
                WebhookSubscriptionId = subscriptionId,
                SkipCount = skipCount,
                MaxResultCount = maxResultCount
            };

            var sql =
                $@"select *
                   from WebhookSendAttempt
                   where WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     and TenantId = @{nameof(sqlParams.TenantId)}
                   order by CreationTime desc
                   offset (@{nameof(sqlParams.SkipCount)}) rows 
                   fetch next (@{nameof(sqlParams.MaxResultCount)}) rows only";

            using (var connection = sqlConnection.Create())
            {
                var items = connection.Query<WebhookSendAttempt>(sql, sqlParams);
                return new PagedResultDto<WebhookSendAttempt> { TotalCount = totalCount, Items = new List<WebhookSendAttempt>(items) };
            }
        }

        public async Task<IPagedResult<WebhookSendAttempt>> GetAllSendAttemptsBySubscriptionAsPagedListAsync(int? tenantId, Guid subscriptionId, int maxResultCount, int skipCount)
        {
            var totalCount = await GetCountSendAttemptsBySubscriptionAsync(tenantId, subscriptionId);

            var sqlParams = new
            {
                TenantId = tenantId,
                WebhookSubscriptionId = subscriptionId,
                SkipCount = skipCount,
                MaxResultCount = maxResultCount
            };

            var sql =
                $@"select *
                   from WebhookSendAttempt
                   where WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     and TenantId = @{nameof(sqlParams.TenantId)}
                   order by CreationTime desc
                   offset (@{nameof(sqlParams.SkipCount)}) rows 
                   fetch next (@{nameof(sqlParams.MaxResultCount)}) rows only";

            using (var connection = sqlConnection.Create())
            {
                var items = await connection.QueryAsync<WebhookSendAttempt>(sql, sqlParams);
                return new PagedResultDto<WebhookSendAttempt> { TotalCount = totalCount, Items = new List<WebhookSendAttempt>(items) };
            }
        }

        public List<WebhookSendAttempt> GetAllSendAttemptsByWebhookEventId(int? tenantId, Guid webhookEventId)
        {
            var sqlParams = new { TenantId = tenantId, WebhookEventId = webhookEventId };
            var sql =
                $@"select * from WebhookSendAttempt
                  where TenantId = @{nameof(sqlParams.TenantId)} and WebhookEventId = @{nameof(sqlParams.WebhookEventId)}";

            using (var connection = sqlConnection.Create())
            {
                return connection.Query<WebhookSendAttempt>(sql, sqlParams).ToList();
            }
        }

        public async Task<List<WebhookSendAttempt>> GetAllSendAttemptsByWebhookEventIdAsync(int? tenantId, Guid webhookEventId)
        {
            var sqlParams = new { TenantId = tenantId, WebhookEventId = webhookEventId };
            var sql =
                $@"select * from WebhookSendAttempt
                  where TenantId = @{nameof(sqlParams.TenantId)} and WebhookEventId = @{nameof(sqlParams.WebhookEventId)}";

            using (var connection = sqlConnection.Create())
            {
                return (await connection.QueryAsync<WebhookSendAttempt>(sql, sqlParams)).ToList();
            }
        }

        public async Task<WebhookSendAttempt> GetAsync(int? tenantId, Guid id)
        {
            var sqlParams = new { TenantId = tenantId, Id = id };
            var sql =
                $@"select * from WebhookSendAttempt
                  where TenantId = @{nameof(sqlParams.TenantId)} and Id = @{nameof(sqlParams.Id)}";

            using (var connection = sqlConnection.Create())
            {
                return (await connection.QueryAsync<WebhookSendAttempt>(sql, sqlParams)).SingleOrDefault();
            }
        }

        public int GetSendAttemptCount(int? tenantId, Guid webhookId, Guid webhookSubscriptionId)
        {
            var sqlParams = new { TenantId = tenantId, WebhookEventId = webhookId, WebhookSubscriptionId = webhookSubscriptionId };
            var sql =
                $@"select count(*) from WebhookSendAttempt
                   where TenantId = @{nameof(sqlParams.TenantId)} 
                     and WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     and WebhookEventId = @{nameof(sqlParams.WebhookEventId)}";

            using (var connection = sqlConnection.Create())
            {
                return connection.QueryFirst<int>(sql, sqlParams);
            }
        }

        public async Task<int> GetSendAttemptCountAsync(int? tenantId, Guid webhookId, Guid webhookSubscriptionId)
        {
            var sqlParams = new { TenantId = tenantId, WebhookEventId = webhookId, WebhookSubscriptionId = webhookSubscriptionId };
            var sql =
                $@"select count(*) from WebhookSendAttempt
                   where TenantId = @{nameof(sqlParams.TenantId)} 
                     and WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     and WebhookEventId = @{nameof(sqlParams.WebhookEventId)}";

            using (var connection = sqlConnection.Create())
            {
                return await connection.QueryFirstAsync<int>(sql, sqlParams);
            }
        }

        public bool HasXConsecutiveFail(int? tenantId, Guid subscriptionId, int searchCount)
        {
            var sqlParams = new { TenantId = tenantId, WebhookSubscriptionId = subscriptionId, SearchCount = searchCount };
            var sql =
                $@"select top(@{nameof(sqlParams.SearchCount)}) *
                   from WebhookSendAttempt
                   where TenantId = @{nameof(sqlParams.TenantId)} 
                     and WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     order by CreationTime desc";

            using (var connection = sqlConnection.Create())
            {
                return connection.Query<WebhookSendAttempt>(sql, sqlParams).All(r => r.ResponseStatusCode != HttpStatusCode.OK);
            }
        }

        public async Task<bool> HasXConsecutiveFailAsync(int? tenantId, Guid subscriptionId, int searchCount)
        {
            var sqlParams = new { TenantId = tenantId, WebhookSubscriptionId = subscriptionId, SearchCount = searchCount };
            var sql =
                $@"select top(@SearchCount) *
                   from WebhookSendAttempt
                   where TenantId = @{nameof(sqlParams.TenantId)} 
                     and WebhookSubscriptionId = @{nameof(sqlParams.WebhookSubscriptionId)}
                     order by CreationTime desc";

            using (var connection = sqlConnection.Create())
            {
                return (await connection.QueryAsync<WebhookSendAttempt>(sql, sqlParams)).All(r => r.ResponseStatusCode != HttpStatusCode.OK);
            }
        }

        public void Insert(WebhookSendAttempt webhookSendAttempt)
        {
            var sql =
                $@"insert into dbo.WebhookSendAttempt (Id, CreationTime, LastModificationTime, Response, ResponseStatusCode, TenantId, WebhookEventId, WebhookSubscriptionId)
                   values 
                   (
                      @{nameof(webhookSendAttempt.Id)}, @{nameof(webhookSendAttempt.CreationTime)},
                      @{nameof(webhookSendAttempt.LastModificationTime)}, @{nameof(webhookSendAttempt.Response)},
                      @{nameof(webhookSendAttempt.ResponseStatusCode)}, @{nameof(webhookSendAttempt.TenantId)},
                      @{nameof(webhookSendAttempt.WebhookEventId)}, @{nameof(webhookSendAttempt.WebhookSubscriptionId)}
                   )";

            using (var connection = sqlConnection.Create())
            {
                connection.Execute(sql, webhookSendAttempt);
            }
        }

        public async Task InsertAsync(WebhookSendAttempt webhookSendAttempt)
        {
            var sql =
                $@"insert into dbo.WebhookSendAttempt (Id, CreationTime, LastModificationTime, Response, ResponseStatusCode, TenantId, WebhookEventId, WebhookSubscriptionId)
                   values 
                   (
                      @{nameof(webhookSendAttempt.Id)}, @{nameof(webhookSendAttempt.CreationTime)},
                      @{nameof(webhookSendAttempt.LastModificationTime)}, @{nameof(webhookSendAttempt.Response)},
                      @{nameof(webhookSendAttempt.ResponseStatusCode)}, @{nameof(webhookSendAttempt.TenantId)},
                      @{nameof(webhookSendAttempt.WebhookEventId)}, @{nameof(webhookSendAttempt.WebhookSubscriptionId)}
                   )";

            using (var connection = sqlConnection.Create())
            {
                await connection.ExecuteAsync(sql, webhookSendAttempt);
            }
        }

        public void Update(WebhookSendAttempt webhookSendAttempt)
        {
            var sql =
            $@"update dbo.WebhookSendAttempt 
               set CreationTime = @{nameof(webhookSendAttempt.CreationTime)},
                   LastModificationTime = @{nameof(webhookSendAttempt.LastModificationTime)}, 
                   Response = @{nameof(webhookSendAttempt.Response)},
                   ResponseStatusCode = @{nameof(webhookSendAttempt.ResponseStatusCode)}, 
                   TenantId = @{nameof(webhookSendAttempt.TenantId)},
                   WebhookEventId = @{nameof(webhookSendAttempt.WebhookEventId)}, 
                   WebhookSubscriptionId = @{nameof(webhookSendAttempt.WebhookSubscriptionId)}
                where Id = @{nameof(webhookSendAttempt.Id)}";

            using (var connection = sqlConnection.Create())
            {
                connection.Execute(sql, webhookSendAttempt);
            }
        }

        public async Task UpdateAsync(WebhookSendAttempt webhookSendAttempt)
        {
            var sql =
            $@"update dbo.WebhookSendAttempt 
               set CreationTime = @{nameof(webhookSendAttempt.CreationTime)},
                   LastModificationTime = @{nameof(webhookSendAttempt.LastModificationTime)}, 
                   Response = @{nameof(webhookSendAttempt.Response)},
                   ResponseStatusCode = @{nameof(webhookSendAttempt.ResponseStatusCode)}, 
                   TenantId = @{nameof(webhookSendAttempt.TenantId)},
                   WebhookEventId = @{nameof(webhookSendAttempt.WebhookEventId)}, 
                   WebhookSubscriptionId = @{nameof(webhookSendAttempt.WebhookSubscriptionId)}
                where Id = @{nameof(webhookSendAttempt.Id)}";

            using (var connection = sqlConnection.Create())
            {
                await connection.ExecuteAsync(sql, webhookSendAttempt);
            }
        }
    }
}
