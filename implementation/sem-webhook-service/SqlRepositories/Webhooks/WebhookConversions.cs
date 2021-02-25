using Domain.Generic;
using Domain.Webhooks;

namespace SqlRepositories.Webhooks
{
    internal static class WebhookConversions
    {
        internal static WebhookInput ToInput(this Webhook webhook) =>
            new WebhookInput(webhook.Id, webhook.TenantId, webhook.EventTypeIds.ToValues(), webhook.PostbackUrl, webhook.Secret);
    }
}
