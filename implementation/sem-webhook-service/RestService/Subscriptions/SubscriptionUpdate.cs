using System;
using System.Collections.Generic;

namespace RestService.Subscriptions
{
    public sealed record SubscriptionUpdate
    (
        Guid SubscriptionId,
        IEnumerable<Guid> EventIds,
        string PostbackUrl,
        string Secret
    );
}