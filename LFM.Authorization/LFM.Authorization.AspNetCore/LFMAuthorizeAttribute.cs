using Microsoft.AspNetCore.Authorization;

namespace LFM.Authorization.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class LfmAuthorizeAttribute: Attribute, IAuthorizeData
{
    private readonly List<ScopedPermissions> _scopedPermissions = new();

    public LfmAuthorizeAttribute() : this(new List<string>().ToArray(), new List<string>().ToArray()) { }
    
    public LfmAuthorizeAttribute(string[] permissions, string[] scopeMask)
    {
        if(permissions.Length != scopeMask.Length)
            throw new ArgumentException("Permissions and scope mask must have the same length.");
        
        for(var i = 0; i < permissions.Length; i++)
            _scopedPermissions.Add(new ScopedPermissions(permissions[i], scopeMask[i]));
    }
    
    public string Policy { get; set; }
    public string Roles { get; set; }
    public string AuthenticationSchemes { get; set; }
}