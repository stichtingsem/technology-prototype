using System;
using System.Collections.Generic;

namespace SqlRepositories.WebhookSubscriptions
{
    internal sealed record WebhookInput(Guid Id, Guid TenantId, IEnumerable<Guid> EventTypeIds, string PostbackUrl, string Secret);
}