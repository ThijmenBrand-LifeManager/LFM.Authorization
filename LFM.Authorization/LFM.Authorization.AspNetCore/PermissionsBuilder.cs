using LFM.Authorization.AspNetCore.Database;
using LFM.Authorization.AspNetCore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Authorization.AspNetCore;

public class PermissionsBuilder(IServiceCollection services)
{
    public PermissionsBuilder InsertPermissions(IEnumerable<InsertPermissionsDto> permissions)
    {
        foreach (var permission in permissions)
        {
            InsertPermission(permission.Name, permission.Category);
        }

        return this;
    }
    
    public PermissionsBuilder InsertPermission(string name, string category)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetService<AuthorizationDbContext>() ?? throw new Exception("could not get AuthorizationDbContext");
        
        var permission = new Permission
        {
            Name = name,
            Category = category,
            Description = "Auto-generated permission"
        };
        context.Permissions.Add(permission);
        context.SaveChanges();

        return this;
    }
}