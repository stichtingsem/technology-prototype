using System;

namespace RestService.Implementations
{
    public interface ITenantRepository
    {
        int? GetTenantId(Guid externalId);
    }
}