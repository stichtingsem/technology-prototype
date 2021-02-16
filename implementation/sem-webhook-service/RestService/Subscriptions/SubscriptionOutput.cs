﻿using RestService.Events;
using System;
using System.Collections.Generic;

namespace RestService.Subscriptions
{
    public sealed record SubscriptionOutput
    (
        Guid SubscriptionId,
        IEnumerable<EventOutput> Events,
        string PostbackUrl,
        string Secret
    );
}