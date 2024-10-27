using LFM.Authorization.AspNetCore.Models;

namespace LFM.Authorization.Endpoint.Models;

public class UniqueDefaultRoles
{
    public string RoleName { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
}