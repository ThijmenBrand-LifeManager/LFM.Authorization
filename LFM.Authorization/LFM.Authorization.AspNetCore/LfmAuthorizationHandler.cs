using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationHandler : AuthorizationHandler<LfmAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, LfmAuthorizationRequirement requirement)
    {
        var user = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (user == null)
        {
            return;
        }
        
        context.Succeed(requirement);
    }
}