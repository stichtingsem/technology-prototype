using System;

namespace RestService.Subscriptions
{
    public record SubscriptionInput
    (
        Guid EventId,
        string PostbackUrl,
        string Secret
    );
}