using RestService.Events;

namespace RestService.Subscriptions
{
    public record SubscriptionDto
    (
        EventDto Event, 
        string PostbackUrl, 
        string Secret
    );
}
