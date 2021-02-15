using Microsoft.Extensions.Configuration;
using System;

namespace RestService.Authorization
{
    public sealed class SchoolIdClaimType
    {
        private readonly string value;

        public SchoolIdClaimType(IConfiguration configuration) => 
            value = configuration["Authorization:SchoolIdClaimType"];

        public bool IsFor(string claimType) => 
            claimType.Equals(value, StringComparison.OrdinalIgnoreCase);
    }
}
