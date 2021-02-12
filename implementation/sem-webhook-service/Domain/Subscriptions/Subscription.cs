using Domain.Events;

namespace RestService.Subscriptions
{
    public sealed record Subscription
    (
        Event Event, 
        string PostbackUrl, 
        string Secret
    );
}
