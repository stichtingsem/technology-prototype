﻿using System;

namespace SqlRepositories.Webhooks
{
    public sealed partial class WebhooksSqlRepository
    {
        public sealed record WebhookOutput(Guid Id, Guid SchoolId, string PostbackUrl, string Secret, Guid EventId);
    }
}
