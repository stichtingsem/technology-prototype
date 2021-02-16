using Domain.Events;
using Domain.Subscriptions;
using RestService.Events;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Subscriptions
{
    public static class SubscriptionConversions
    {
        public static IEnumerable<SubscriptionOutput> ToOutput(this IEnumerable<Subscription> subscriptions, IEnumerable<Event> events) =>
            subscriptions.Select(subscription => subscription.ToOutput(events));

        public static SubscriptionOutput ToOutput(this Subscription subscription, IEnumerable<Event> events) =>
            subscription.Convert((subscriptionId, eventIds, postbackUrl, secret) =>
                new SubscriptionOutput(subscriptionId, events.Where(e => eventIds.Any(eventId => e.IsFor(eventId))).ToOutput(), postbackUrl, secret));
    }
}
