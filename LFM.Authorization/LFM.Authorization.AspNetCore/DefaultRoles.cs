namespace LFM.Authorization.AspNetCore;

public class DefaultRoles
{
    private DefaultRoles(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    
    public static DefaultRoles WorkstreamAdmin => new DefaultRoles("WorkstreamAdmin");
    public static DefaultRoles WorkstreamReader => new DefaultRoles("WorkstreamReader");
    public static DefaultRoles WorkstreamConfigurer => new DefaultRoles("WorkstreamConfigurer");
    
    public static DefaultRoles ProjectAdmin => new DefaultRoles("ProjectAdmin");
    public static DefaultRoles ProjectReader => new DefaultRoles("ProjectReader");
    public static DefaultRoles ProjectConfigurer => new DefaultRoles("ProjectConfigurer");
    public static DefaultRoles ProjectUser => new DefaultRoles("ProjectUser");
    
    public override string ToString() => Value;
}