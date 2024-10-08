using LFM.Authorization.Core.Models;

namespace LFM.Authorization.Repository.Interfaces;

public interface IPermissionRepository
{
    Task<Permission> CreateAsync(Permission permission, CancellationToken cancellationToken);
    Task<Permission?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Permission>> ListAsync(CancellationToken cancellationToken);   
}