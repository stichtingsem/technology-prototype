using Domain.Generic;

namespace Domain.Schools
{
    public sealed class SchoolId : ValueObject<string>
    {
        SchoolId(string value) : base(value) { }

        public static implicit operator SchoolId(string value) => new SchoolId(value);
    }
}
