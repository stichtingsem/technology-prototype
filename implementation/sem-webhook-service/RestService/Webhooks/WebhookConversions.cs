using Domain.Events;
using Domain.Webhooks;
using RestService.Events;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Webhooks
{
    public static class WebhookConversions
    {
        public static IEnumerable<WebhookOutput> ToOutput(this IEnumerable<Webhook> webhooks, IEnumerable<Event> events) =>
            webhooks.Select(webhook => webhook.ToOutput(events));

        public static WebhookOutput ToOutput(this Webhook webhook, IEnumerable<Event> events) =>
            webhook.Convert((webhookId, schoolId, eventIds, postbackUrl, secret) =>
                new WebhookOutput(webhookId, events.Where(ev => eventIds.Any(eventId => ev.Id == eventId)).ToOutput(), postbackUrl, secret));
    }
}
