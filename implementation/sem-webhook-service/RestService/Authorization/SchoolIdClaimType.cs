using Microsoft.Extensions.Configuration;

namespace RestService.Authorization
{
    public sealed class SchoolIdClaimType
    {
        private readonly string schoolIdClaimType;

        public SchoolIdClaimType(IConfiguration configuration) => 
            schoolIdClaimType = configuration["Authorization:SchoolIdClaimType"];

        public static implicit operator string(SchoolIdClaimType s) => s.schoolIdClaimType;
    }
}
