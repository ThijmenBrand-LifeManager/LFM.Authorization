using Microsoft.AspNetCore.Authorization;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizationRequirement(List<ScopedPermissions> permissions) : IAuthorizationRequirement
{
    private List<ScopedPermissions> Permissions { get; } = permissions;
}