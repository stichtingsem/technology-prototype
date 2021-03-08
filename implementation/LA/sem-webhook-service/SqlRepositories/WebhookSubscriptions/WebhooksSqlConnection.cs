using SqlRepositories.WebhookSubscriptions;

namespace SqlRepositories.WebhookSubscriptions
{
    public sealed class WebhooksSqlConnection : SqlConnectionCreator
    {
        public WebhooksSqlConnection(WebhooksSqlConnectionString connectionString) : base(connectionString)
        {
        }
    }
}
