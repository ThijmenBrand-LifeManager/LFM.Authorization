using System.Security.Claims;
using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.AspNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IServiceScopeFactory serviceScopeFactory) : AuthorizationHandler<LfmAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, LfmAuthorizationRequirement requirement)
    {
        if (context.User.Identity is not ClaimsIdentity { IsAuthenticated: true })
            return;
        
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
            throw new Exception("Invalid user id");
        
        if (requirement.Permissions.Count == 0)
        {
            context.Succeed(requirement);
            return;
        }

        var substituteScopePermissions = requirement.Permissions.Select(permission => new SubstituteScopePermission()
        {
            Permission = permission.Permission,
            Scope = GetScope(permission.ScopeMask)
        }).ToList();

        using var scope = serviceScopeFactory.CreateScope();
        var validatePermissions = scope.ServiceProvider.GetRequiredService<IValidatePermissions>();
        
        var result = await validatePermissions.ValidatePermissionsAsync(userId, substituteScopePermissions);
        if(!result)
            context.Fail();
        else
            context.Succeed(requirement);
    }

    private string GetScope(string scopeMask)
    {
        var scope = scopeMask;
        var scopeVariables = ScopeHelper.GetScopeVariables(scopeMask);
        foreach (var scopeVariable in scopeVariables)
        {
            var routeValue = GetRouteParameterValue(scopeVariable);
            scope = scope.Replace($"{{{scopeVariable}}}", routeValue);
        }
        return scope;
    }
    
    private string GetRouteParameterValue(string parameterName)
    {
        var parameterValue = httpContextAccessor.HttpContext?.GetRouteValue(parameterName);
        if (parameterValue == null)
            throw new Exception($"Route parameter {parameterName} not found");
        return parameterValue.ToString()!;
    }
}