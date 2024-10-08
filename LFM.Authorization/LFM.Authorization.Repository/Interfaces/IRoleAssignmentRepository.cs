using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;

namespace LFM.Authorization.Repository.Interfaces;

public interface IRoleAssignmentRepository
{
    Task<RoleAssignment> CreateAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken);
    void DeleteAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken);
    Task<IEnumerable<RoleAssignment>> GetAllForUserByScope(string userId, CancellationToken cancellationToken);
}