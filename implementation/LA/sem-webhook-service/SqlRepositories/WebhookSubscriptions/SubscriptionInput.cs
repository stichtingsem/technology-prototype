using System;

namespace SqlRepositories.WebhookSubscriptions
{
    internal sealed record SubscriptionInput(Guid WebhookId, Guid EventTypeId);
}
