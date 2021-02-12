using System;

namespace RestService.Subscriptions
{
    public record SubscriptionDto(Guid EventId, string PostbackUrl, string Secret);
}
