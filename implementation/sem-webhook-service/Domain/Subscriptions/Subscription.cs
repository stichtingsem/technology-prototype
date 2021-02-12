using System;

namespace RestService.Subscriptions
{
    public sealed record Subscription
    (
        string SchoolId,
        Guid EventId, 
        string PostbackUrl, 
        string Secret
    );
}
