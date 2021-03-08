using RestService.EventTypes;
using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    public sealed record WebhookOutput
    (
        Guid Id,
        IEnumerable<EventTypeOutput> Events,
        string PostbackUrl,
        string Secret
    );
}