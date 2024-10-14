using Microsoft.AspNetCore.Authorization;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationRequirement(List<ScopedPermission> permissions) : IAuthorizationRequirement
{
    public List<ScopedPermission> Permissions { get; init; } = permissions;
}