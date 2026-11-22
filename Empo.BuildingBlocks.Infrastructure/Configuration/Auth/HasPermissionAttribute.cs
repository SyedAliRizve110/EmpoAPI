using Microsoft.AspNetCore.Authorization;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.Auth;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
        : base(policy: PermissionPolicyProvider.PolicyPrefix + permission)
    {
    }
}
