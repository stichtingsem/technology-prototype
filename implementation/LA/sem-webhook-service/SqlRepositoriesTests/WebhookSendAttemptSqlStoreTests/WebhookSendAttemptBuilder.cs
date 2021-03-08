using Abp.Webhooks;
using System;
using System.Net;

namespace SqlRepositoriesTests.WebhookSendAttemptSqlStoreTests
{
    public class WebhookSendAttemptBuilder
    {
        SqlTenantId tenantId = Guid.NewGuid().GetHashCode();
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        DateTime creationTime = new DateTime(2020, 02, 21);

        public WebhookSendAttempt Build(Guid webhookEventId, Guid webhookSubscriptionId) => new WebhookSendAttempt
        {
            Id = Guid.NewGuid(),
            CreationTime = creationTime,
            LastModificationTime = creationTime,
            Response = "aResponse",
            ResponseStatusCode = httpStatusCode,
            TenantId = tenantId,
            WebhookEventId = webhookEventId,
            WebhookSubscriptionId = webhookSubscriptionId
        };

        public WebhookSendAttemptBuilder WithTenantId(int? tenantId)
        {
            this.tenantId = tenantId;

            return this;
        }

        public WebhookSendAttemptBuilder WithHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            this.httpStatusCode = httpStatusCode;

            return this;
        }

        internal WebhookSendAttemptBuilder WithCreationTime(DateTime creationTime)
        {
            this.creationTime = creationTime;

            return this;
        }
    }
}