using LFM.Authorization.AspNetCore.Models;

namespace LFM.Authorization.Repository.Interfaces;

public interface IDefaultRolePermissionRepository
{
    Task<IEnumerable<DefaultRolePermission>> ListAsync(CancellationToken cancellationToken);
}