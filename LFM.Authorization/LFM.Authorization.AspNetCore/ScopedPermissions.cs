namespace LFM.Authorization.AspNetCore;

public class ScopedPermission(string permission, string scopeMask)
{
    public string Permission { get; set; } = permission;
    public string ScopeMask { get; set; } = scopeMask;
}