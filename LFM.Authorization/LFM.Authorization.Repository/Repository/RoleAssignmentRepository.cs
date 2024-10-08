using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LFM.Authorization.Repository.Repository;

public class RoleAssignmentRepository(DatabaseContext context) : RepositoryBase<RoleAssignment>(context), IRoleAssignmentRepository
{
    public void DeleteAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken)
    {
        Context.RoleAssignments.Remove(roleAssignment);
        Context.SaveChanges();
    }

    public async Task<IEnumerable<RoleAssignment>> GetAllForUserByScope(string userId, CancellationToken cancellationToken)
    {
        var result = await Context.RoleAssignments
            .Include(r => r.Role)
            .Include(r => r.Role.Permissions)
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);

        return result;
    }
}