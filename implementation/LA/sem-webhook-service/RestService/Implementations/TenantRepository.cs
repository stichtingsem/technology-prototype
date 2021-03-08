using System;
using System.Collections.Generic;

namespace RestService.Implementations
{
    public class TenantRepository : ITenantRepository
    {
        private static Dictionary<Guid, int> _tenants = new Dictionary<Guid, int>();

        public int? GetTenantId(Guid externalId)
        {
            if (!_tenants.ContainsKey(externalId))
            {
                _tenants.Add(externalId, _tenants.Count);
            }
            
            return _tenants[externalId];
        }
    }
}
