using Domain.Generic;
using System;

namespace Domain.Webhooks
{
    public class WebhookId : ValueObject<Guid>
    {
        public static WebhookId Create() => new WebhookId(Guid.NewGuid());

        public WebhookId(Guid value) : base(value) { }

        public static implicit operator WebhookId(Guid value) => new WebhookId(value);
    }
}
