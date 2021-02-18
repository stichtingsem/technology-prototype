using System;
using System.Collections.Generic;

namespace SqlRepositories.Webhooks
{
    internal sealed record WebhookInput(Guid Id, Guid SchoolId, IEnumerable<Guid> EventIds, string PostbackUrl, string Secret);
}