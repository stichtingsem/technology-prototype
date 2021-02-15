using Domain.Generic;
using Domain.Schools;
using Microsoft.AspNetCore.Http;
using RestService.Authorization;
using System;
using System.Linq;

namespace RestService.Schools
{
    public sealed class SchoolFromHttpContext : ISchool
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SchoolIdClaimType schoolClaimType;

        public SchoolFromHttpContext(IHttpContextAccessor httpContextAccessor, SchoolIdClaimType schoolClaimType)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.schoolClaimType = schoolClaimType;
        }

        // How to deal with a user that does not have a 
        // school id claim type?
        public SchoolId Id => 
            httpContextAccessor.HttpContext.User.Claims.ToList().
                Single(claim => schoolClaimType.IsFor(claim.Type)).Value.ToGuid();
    }
}
