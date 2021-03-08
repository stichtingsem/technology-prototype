using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    public sealed record WebhookPut
    (
        Guid Id,
        IEnumerable<Guid> EventIds,
        string PostbackUrl,
        string Secret
    );
}