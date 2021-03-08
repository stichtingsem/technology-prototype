using Domain.Generic;

namespace Domain.Webhooks
{
    public sealed class PostbackUrl : ValueObject<string>
    {
        public PostbackUrl(string value) : base(value) { }

        public static implicit operator PostbackUrl(string value) => new PostbackUrl(value);
    }
}
