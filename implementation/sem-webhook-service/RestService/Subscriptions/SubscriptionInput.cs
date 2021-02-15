﻿using System;

namespace RestService.Subscriptions
{
    public sealed record SubscriptionInput
    (
        Guid EventId,
        string PostbackUrl,
        string Secret
    );
}