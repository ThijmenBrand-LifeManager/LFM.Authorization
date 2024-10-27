using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Repository.Repository;

public class RoleRepository(DatabaseContext context) : RepositoryBase<LfmRole>(context), IRoleRepository
{
    public async Task<LfmRole?> GetByNameAndScopeAsync(string name, string scope, CancellationToken cancellationToken = default)
    {
        return await Context.Roles
            .Include(r => r.Permissions)
            .Where(r => r.Name == name)
            .Where(r => r.Scope == scope)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<LfmRole> CreateIfNotExistsAsync(LfmRole role, CancellationToken cancellationToken = default)
    {
        var existingRole = await GetByNameAndScopeAsync(role.Name, role.Scope, cancellationToken);
        if (existingRole != null)
        {
            return existingRole;
        }
        
        var entity = await Context.Roles.AddAsync(role, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }
    
    public override async Task<IEnumerable<LfmRole>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Roles
            .Include(r => r.Permissions)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<LfmRole?> GetByNameAndScope(string name, string scope, CancellationToken cancellationToken)
    {
        return await Context.Roles.Where(x => x.Name == name && x.Scope == scope).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<LfmRole>> ListByScopeAsync(string scope, CancellationToken cancellationToken)
    {
        return await Context.Roles.Where(r => r.Scope == scope).ToListAsync(cancellationToken);
    }
}