using Domain.Generic;

namespace Domain.Subscriptions
{
    public sealed class PostbackUrl : ValueObject<string>
    {
        PostbackUrl(string value) : base(value) { }

        public static implicit operator PostbackUrl(string value) => new PostbackUrl(value);
    }
}
