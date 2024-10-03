namespace LFM.Authorization.AspNetCore;

public class ScopedPermissions(string permissions, string scopeMask)
{
    public string Permissions { get; private set; } = permissions;
    public string ScopeMask { get; private set; } = scopeMask;
}