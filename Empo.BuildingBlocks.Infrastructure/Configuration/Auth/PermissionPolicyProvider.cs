using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.Auth;

/// <summary>
/// Automatically builds an authorization policy dynamically for any "Permission:{name}" requirement.
/// No need to register individual permission policies in Program.cs.
/// </summary>
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
        if (policyName.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase))
        {
            var permission = policyName.Substring(PolicyPrefix.Length);
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(permission));
            return Task.FromResult<AuthorizationPolicy?>(policy.Build());
        }

        return _fallback.GetPolicyAsync(policyName);
    }
}
