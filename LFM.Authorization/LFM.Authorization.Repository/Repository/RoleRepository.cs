using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Repository.Repository;

public class RoleRepository(DatabaseContext context) : RepositoryBase<LfmRole>(context), IRoleRepository
{
    public override async Task<LfmRole?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await Context.Roles
            .Include(r => r.Permissions)
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
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