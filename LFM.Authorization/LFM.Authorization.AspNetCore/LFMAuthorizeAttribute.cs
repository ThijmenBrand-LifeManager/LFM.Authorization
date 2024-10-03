using Microsoft.AspNetCore.Authorization;

namespace LFM.Authorization.AspNetCore;

public class LfmAuthorizeRequirement : IAuthorizationRequirement
{
    private readonly List<ScopedPermissions> _scopedPermissions = new();

    public LfmAuthorizeRequirement() : this(new List<string>().ToArray(), new List<string>().ToArray()) { }
    
    public LfmAuthorizeRequirement(string[] permissions, string[] scopeMask)
    {
        if(permissions.Length != scopeMask.Length)
            throw new ArgumentException("Permissions and scope mask must have the same length.");
        
        for(var i = 0; i < permissions.Length; i++)
            _scopedPermissions.Add(new ScopedPermissions(permissions[i], scopeMask[i]));
    }
}