using Domain.Generic;
using System;

namespace Domain.Tenants
{
    public sealed class TenantId : ValueObject<Guid>
    {
        public TenantId(Guid value) : base(value) { }

        public static implicit operator TenantId(Guid value) => new TenantId(value);
    }
}
