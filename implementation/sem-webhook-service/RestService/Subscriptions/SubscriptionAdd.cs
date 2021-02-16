using System;
using System.Collections.Generic;

namespace RestService.Subscriptions
{
    public sealed record SubscriptionAdd
    (
        IEnumerable<Guid> EventIds,
        string PostbackUrl,
        string Secret
    );
}