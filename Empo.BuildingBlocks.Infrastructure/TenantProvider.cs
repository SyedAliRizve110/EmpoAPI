
using Empo.BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Empo.BuildingBlocks.Infrastructure;

public class TenantProvider : ITenantProvider
{
    private IHttpContextAccessor _contextAccessor;
    public TenantProvider(IHttpContextAccessor contextAccessor)
    {
            _contextAccessor = contextAccessor;
    }

    #region Tenant

    public Guid GetTenantId()
    {
        var _httpContext = _contextAccessor.HttpContext;
        if (_httpContext!= null)
        {
            if (_httpContext.Request.Headers.TryGetValue("Tenant-Id", out var tenantId))
            {
                return new Guid(tenantId);
            }
        }
        return Guid.Empty;
    }
    #endregion
}
