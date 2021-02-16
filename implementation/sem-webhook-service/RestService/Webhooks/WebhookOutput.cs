using RestService.Events;
using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    public sealed record WebhookOutput
    (
        Guid Id,
        IEnumerable<EventOutput> Events,
        string PostbackUrl,
        string Secret
    );
}