using Domain.Generic;
using Domain.Tenants;
using Microsoft.AspNetCore.Http;
using RestService.Authorization;
using System.Linq;

namespace RestService.Tenants
{
    public sealed class TenantFromHttpContext : ITenant
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TenantIdClaimType tenantIdClaimType;

        public TenantFromHttpContext(IHttpContextAccessor httpContextAccessor, TenantIdClaimType tenantIdClaimType)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tenantIdClaimType = tenantIdClaimType;
        }

        // How to deal with a user that does not have a 
        // tenant id claim type?
        public TenantId Id => 
            httpContextAccessor.HttpContext.User.Claims.ToList().
                Single(claim => tenantIdClaimType.IsFor(claim.Type)).Value.ToGuid();
    }
}
