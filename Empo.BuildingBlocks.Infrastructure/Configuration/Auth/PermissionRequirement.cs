using Microsoft.AspNetCore.Authorization;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.Auth;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
