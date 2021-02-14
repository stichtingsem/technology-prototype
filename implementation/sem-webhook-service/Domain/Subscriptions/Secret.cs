using Domain.Generic;

namespace Domain.Subscriptions
{
    public sealed class Secret : ValueObject<string>
    {
        Secret(string value) : base(value) { }

        public static implicit operator Secret(string value) => new Secret(value);
    }
}
