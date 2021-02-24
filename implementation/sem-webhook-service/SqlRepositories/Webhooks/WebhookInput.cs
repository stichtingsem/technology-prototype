using System;
using System.Collections.Generic;

namespace SqlRepositories.Webhooks
{
    internal sealed record WebhookInput(Guid Id, Guid TenantId, IEnumerable<Guid> EventTypeIds, string PostbackUrl, string Secret);
}