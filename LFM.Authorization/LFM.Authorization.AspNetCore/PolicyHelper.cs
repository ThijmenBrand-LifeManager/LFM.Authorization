using Newtonsoft.Json;

namespace LFM.Authorization.AspNetCore;

public static class PolicyHelper
{
    private const string NoRequirements = "NoRequirements";

    public static string ToPolicy(this List<ScopedPermission> scopedPermissionsList)
    {
        if (!scopedPermissionsList.Any()) return NoRequirements;
        return JsonConvert.SerializeObject(scopedPermissionsList);
    }

    public static List<ScopedPermission>? ToScopedPermissions(this string policy)
    {
        try
        {
            if (policy == NoRequirements) return new List<ScopedPermission>();
            return JsonConvert.DeserializeObject<List<ScopedPermission>>(policy);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}