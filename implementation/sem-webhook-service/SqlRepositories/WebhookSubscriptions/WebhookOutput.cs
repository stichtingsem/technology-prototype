using System;

namespace SqlRepositories.WebhookSubscriptions
{
    public sealed record WebhookOutput(Guid Id, Guid TenantId, string PostbackUrl, string Secret, Guid EventTypeId);
}
