using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        
        return policy ?? GetPolicy(policyName); 
    }

    private AuthorizationPolicy? GetPolicy(string policyName)
    {
            var scopePermissions = policyName.ToScopedPermissions();
            return new AuthorizationPolicyBuilder()
                    .AddRequirements(new LfmAuthorizationRequirement(scopePermissions))
                    .Build();
    }
}