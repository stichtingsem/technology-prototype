using Domain.Events;

namespace RestService.Subscriptions
{
    public sealed record SubscriptionDto
    (
        Event Event, 
        string PostbackUrl, 
        string Secret
    );
}
