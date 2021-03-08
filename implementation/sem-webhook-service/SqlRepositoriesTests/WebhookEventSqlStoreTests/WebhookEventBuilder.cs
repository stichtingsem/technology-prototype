using Abp.Webhooks;
using System;

namespace SqlRepositoriesTests.WebhookEventSqlStoreTests
{
    public class WebhookEventBuilder
    {
        public WebhookEvent Build() => new WebhookEvent
        { 
            Id = Guid.NewGuid(),
            TenantId = Guid.NewGuid().GetHashCode(),
            CreationTime = new DateTime(2020, 02, 20),
            WebhookName = "aWebhookName",
            Data = "{}"
        };
    }
}
