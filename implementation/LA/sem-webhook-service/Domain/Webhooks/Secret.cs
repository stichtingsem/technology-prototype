using Domain.Generic;

namespace Domain.Webhooks
{
    public sealed class Secret : ValueObject<string>
    {
        public Secret(string value) : base(value) { }

        public static implicit operator Secret(string value) => new Secret(value);
    }
}
