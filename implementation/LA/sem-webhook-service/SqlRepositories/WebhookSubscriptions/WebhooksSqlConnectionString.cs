using Domain.Generic;

namespace SqlRepositories.WebhookSubscriptions
{
    public sealed class WebhooksSqlConnectionString : ValueObject<string>
    {
        public WebhooksSqlConnectionString(string value) : base(value) { }

        public static implicit operator WebhooksSqlConnectionString(string value) => new WebhooksSqlConnectionString(value);
    }
}