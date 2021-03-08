using Domain.Generic;

namespace SqlRepositoriesTests
{
    public sealed class SqlTenantId : ValueObject<int?>
    {
        SqlTenantId(int? value) : base(value)
        {
        }

        public static implicit operator SqlTenantId(int? value) => new SqlTenantId(value);
    }
}
