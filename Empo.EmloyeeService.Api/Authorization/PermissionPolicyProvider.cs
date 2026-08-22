using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Empo.EmloyeeService.Api.Authorization;

// Any [Authorize(Policy = "Permission:Employee.Create")] policy is generated on the fly —
// you never need to register each permission policy by hand in Program.cs.
public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public const string PolicyPrefix = "Permission:";
    private readonly DefaultAuthorizationPolicyProvider _fallback;

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        _fallback = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallback.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => _fallback.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(PolicyPrefix))
        {
            var permission = policyName.Substring(PolicyPrefix.Length);
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(permission));
            return Task.FromResult<AuthorizationPolicy?>(policy.Build());
        }

        return _fallback.GetPolicyAsync(policyName);
    }
}
