using LFM.Authorization.AspNetCore.Models;

namespace LFM.Authorization.AspNetCore.Services;

public interface IValidatePermissions
{
    Task<bool> ValidatePermissionsAsync(string userId, List<SubstituteScopePermission> permissions, CancellationToken cancellationToken = default);
}