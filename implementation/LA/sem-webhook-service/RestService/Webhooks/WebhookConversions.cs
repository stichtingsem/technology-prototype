using Domain.EventTypes;
using Domain.Webhooks;
using RestService.EventTypes;
using System.Collections.Generic;
using System.Linq;

namespace RestService.Webhooks
{
    public static class WebhookConversions
    {
        public static IEnumerable<WebhookOutput> ToOutput(this IEnumerable<Webhook> webhooks, IEnumerable<EventType> events) =>
            webhooks.Select(webhook => webhook.ToOutput(events));

        public static WebhookOutput ToOutput(this Webhook webhook, IEnumerable<EventType> events) =>
            new WebhookOutput(webhook.Id, events.Where(ev => webhook.HasEventType(ev.Id)).ToOutput(), webhook.PostbackUrl, webhook.Secret);
    }
}
