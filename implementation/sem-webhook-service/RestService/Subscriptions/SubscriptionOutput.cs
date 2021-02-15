using System;

namespace RestService.Subscriptions
{
    public sealed record SubscriptionOutput
    (
        Guid EventId,
        string EventName,
        string PostbackUrl,
        string Secret
    );
}