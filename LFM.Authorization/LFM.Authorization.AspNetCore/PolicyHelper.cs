using Newtonsoft.Json;

namespace LFM.Authorization.AspNetCore;

public static class PolicyHelper
{
    private const string NoRequirements = "NoRequirements";

    public static string ToPolicy(this List<ScopedPermissions> scopedPermissionsList)
    {
        if (!scopedPermissionsList.Any()) return NoRequirements;
        return JsonConvert.SerializeObject(scopedPermissionsList);
    }

    public static List<ScopedPermissions>? ToScopedPermissions(this string policy)
    {
        try
        {
            if (policy == NoRequirements) return new List<ScopedPermissions>();
            return JsonConvert.DeserializeObject<List<ScopedPermissions>>(policy);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}