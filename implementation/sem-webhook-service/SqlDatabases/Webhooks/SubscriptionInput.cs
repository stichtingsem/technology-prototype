using System;

namespace SqlRepositories.Webhooks
{
    internal sealed record SubscriptionInput(Guid WebhookId, Guid EventId);
}
