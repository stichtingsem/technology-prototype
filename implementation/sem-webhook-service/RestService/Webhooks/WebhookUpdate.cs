using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    public sealed record WebhookUpdate
    (
        Guid WebhookId,
        IEnumerable<Guid> EventIds,
        string PostbackUrl,
        string Secret
    );
}