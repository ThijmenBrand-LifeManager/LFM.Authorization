using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Repository.Repository;

public class DefaultRolePermissionRepository(DatabaseContext context) : IDefaultRolePermissionRepository
{
    public async Task<IEnumerable<DefaultRolePermission>> ListAsync(CancellationToken cancellationToken)
    {
        var defaultRolePermissions = await context.DefaultRolePermissions
            .Include(x => x.Permission)
            .ToListAsync(cancellationToken);
        
        return defaultRolePermissions;
    }
}