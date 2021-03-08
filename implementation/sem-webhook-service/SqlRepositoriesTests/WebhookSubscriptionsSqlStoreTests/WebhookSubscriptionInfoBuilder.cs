using Abp.Webhooks;
using System;

namespace SqlRepositoriesTests.WebhookSubscriptionsSqlStoreTests
{
    public class WebhookSubscriptionInfoBuilder
    {
        SqlTenantId tenantId = Guid.NewGuid().GetHashCode();

        public WebhookSubscriptionInfo Build() => new WebhookSubscriptionInfo
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            CreationTime = new DateTime(2020, 01, 01),
            IsActive = true,
            WebhookUri = "https://some.uri",
            Secret = "aSecret"
        };

        public WebhookSubscriptionInfoBuilder WithTenantId(int? tenantId)
        {
            this.tenantId = tenantId;
            return this;
        }
    }
}
