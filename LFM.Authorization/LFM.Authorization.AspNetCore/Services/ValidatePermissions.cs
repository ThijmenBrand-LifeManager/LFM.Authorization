using System.Runtime.CompilerServices;
using LFM.Authorization.AspNetCore.Database;
using LFM.Authorization.AspNetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.AspNetCore.Services;

//TODO: If we have a lot of users, we should consider caching the role assignments, or role permissions
public class ValidatePermissions(AuthorizationDbContext context) : IValidatePermissions
{
    public async Task<bool> ValidatePermissionsAsync(string userId, List<SubstituteScopePermission> scopedPermissions, CancellationToken cancellationToken = default)
    {
        var roleAssignments = await context.Set<RoleAssignment>().Where(x => x.UserId == userId)
            .Include(roleAssignment => roleAssignment.Role)
            .Include(roleAssignment => roleAssignment.Role.Permissions)
            .ToListAsync(cancellationToken);

        var isAllowed = false;

        foreach (var applicableRoles in scopedPermissions.Select(requirement => roleAssignments.Where(role => scopedPermissions.Any(x => x.Scope.isChildScope(role.Scope)))))
        {
            isAllowed = CheckPermissions(applicableRoles.Select(x => x.Role), scopedPermissions);
            if (isAllowed)
                break;
        }
        
        return isAllowed;
    }
    
    private static bool CheckPermissions(IEnumerable<LfmRole> roles, IEnumerable<SubstituteScopePermission> scopedPermissions)
    {
        return roles.Any(role => role.Permissions.Any(x => scopedPermissions.Any(y => y.Permission.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))));
    }
}