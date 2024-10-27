using LFM.Authorization.AspNetCore.Database;
using LFM.Authorization.AspNetCore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.AspNetCore;

public class PermissionsBuilder(IServiceCollection services, AuthorizationDbContext context)
{
    public PermissionsBuilder InsertPermissions(InsertMultiplePermissionsOnMultipleRolesDto options)
    {
        foreach (var permission in options.Permissions)
        {
            InsertPermission(permission.Name, permission.Category);
        }

        foreach (var role in options.Roles)
        {
            AddDefaultRolePermissions(role, options.Permissions);
        }

        return this;
    }

    public PermissionsBuilder InsertPermissions(InsertMultiplePermissionsOnSingleRoleDto options)
    {
        foreach (var permission in options.Permissions)
        {
            InsertPermission(permission.Name, permission.Category);
        }
        
        AddDefaultRolePermissions(options.Role, options.Permissions);

        return this;
    }

    public PermissionsBuilder InsertPermissions(InsertPermissionOnMultipleRolesDto options)
    {
        InsertPermission(options.Permission.Name, options.Permission.Category);
        
        foreach (var role in options.Roles)
        {
            AddDefaultRolePermissions(role, new[] { options.Permission });
        }

        return this;
    }

    public PermissionsBuilder InsertPermissions(InsertPermissionOnSingleRoleDto options)
    {
        InsertPermission(options.Permission.Name, options.Permission.Category);
        
        AddDefaultRolePermissions(options.Role, new[] { options.Permission });

        return this;
    }
    
    private void AddDefaultRolePermissions(DefaultRoles role, IEnumerable<PermissionDto> permissions)
    {
        foreach (var permission in permissions)
        {
            InsertDefaultRolePermission(role, permission.Name);
        }
    }
    
    private void InsertDefaultRolePermission(DefaultRoles role, string permissionName)
    {
        var defaultAssignmentExists = context.DefaultRolePermissions.Any(drp =>
            drp.Role == role.ToString() && drp.PermissionName == permissionName);
        if (defaultAssignmentExists) return;

        context.DefaultRolePermissions.Add(new DefaultRolePermission
        {
            Role = role.ToString(),
            PermissionName = permissionName
        });
        
        context.SaveChanges();
    }
    
    private void InsertPermission(string name, string category)
    {
        var permission = new Permission
        {
            Name = name,
            Category = category,
            Description = "Auto-generated permission"
        };
        var exists = context.Permissions.Any(p => p.Name == permission.Name);
        
        if (!exists)
        {
            context.Permissions.Add(permission);
        }

        context.SaveChanges();
    }
}