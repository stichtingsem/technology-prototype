using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    public sealed record WebhookPost
    (
        IEnumerable<Guid> EventIds,
        string PostbackUrl,
        string Secret
    );
}