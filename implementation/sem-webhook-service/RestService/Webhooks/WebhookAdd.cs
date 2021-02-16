using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    public sealed record WebhookAdd
    (
        IEnumerable<Guid> EventIds,
        string PostbackUrl,
        string Secret
    );
}