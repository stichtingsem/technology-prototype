using Domain.Events;
using Domain.Subscriptions;
using RestService.Events;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Subscriptions
{
    public static class SubscriptionExtensions
    {
        public static SubscriptionOutput ToOutput(this Subscription subscription, EventOutput eventOutput) => subscription.Convert((eventId, postbackUrl, secret) => 
            new SubscriptionOutput(eventId, eventOutput.Name, postbackUrl, secret));

        // What if we can't find a corresponding event id?
        public static IEnumerable<SubscriptionOutput> ToOutput(this IEnumerable<Subscription> subscriptions, IEnumerable<Event> events) => 
            subscriptions.Select(subscription => subscription.ToOutput(events.Single(e => subscription.IsFor(e)).ToOutput())).ToList();
    }
}
