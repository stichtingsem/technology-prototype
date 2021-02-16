using Domain.Generic;
using System;

namespace Domain.Schools
{
    public sealed class SchoolId : ValueObject<Guid>
    {
        public SchoolId(Guid value) : base(value) { }

        public static implicit operator SchoolId(Guid value) => new SchoolId(value);
    }
}
