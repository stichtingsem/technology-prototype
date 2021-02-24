using Microsoft.Extensions.Configuration;
using System;

namespace RestService.Authorization
{
    public sealed class TenantIdClaimType
    {
        private readonly string value;

        public TenantIdClaimType(IConfiguration configuration) => 
            value = configuration["Authorization:TenantIdClaimType"];

        public bool IsFor(string claimType) => 
            claimType.Equals(value, StringComparison.OrdinalIgnoreCase);
    }
}
