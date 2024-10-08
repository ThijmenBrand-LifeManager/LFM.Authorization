using LFM.Authorization.Core.Models;

namespace LFM.Authorization.Repository.Interfaces;

public interface IRoleRepository
{
    Task<LfmRole> CreateAsync(LfmRole role, CancellationToken cancellationToken);
    Task UpdateAsync(CancellationToken cancellationToken);
    Task<LfmRole?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<LfmRole?> GetByNameAndScope(string name, string scope, CancellationToken cancellationToken);
    Task<IEnumerable<LfmRole>> ListAsync(CancellationToken cancellationToken);
    Task<IEnumerable<LfmRole>> ListByScopeAsync(string scope, CancellationToken cancellationToken);
}