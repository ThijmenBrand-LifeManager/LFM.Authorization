using System.Text.RegularExpressions;

namespace LFM.Authorization.AspNetCore.Services;

public static partial class ScopeHelper
{
    public const string ScopeMaskGlobal = $"{ScopeLevelSeperator}platform";
    public const string ScopeMaskWorkStream = $"{ScopeLevelSeperator}workstream{ScopeLevelSeperator}{ScopeVariableStart}workstreamId{ScopeVariableEnd}";
    public const string ScopeMaskProject = $"{ScopeMaskWorkStream}{ScopeLevelSeperator}project{ScopeLevelSeperator}{ScopeVariableStart}projectId{ScopeVariableEnd}";
    
    private const string ScopeVariableStart = "{";
    private const string ScopeVariableEnd = "}";
    private const string ScopeLevelSeperator = "/";
    private static readonly Regex ScopeVariableRegex = ScopeRegex();
    
    public static IEnumerable<string> GetScopeVariables(string scopeVariable)
    {
        var matches = ScopeVariableRegex.Matches(scopeVariable);
        return matches.Select(m => m.Value.Replace(ScopeVariableStart, string.Empty).Replace(ScopeVariableEnd, string.Empty));
    }
    
    public static string CreateWorkstreamScope(string workstreamId) =>
        $"{ScopeMaskWorkStream.Replace(ScopeVariableStart + "workstreamId" + ScopeVariableEnd, workstreamId)}";
    
    public static string CreateProjectScope(string workstreamId, string projectId) => 
        $"{CreateWorkstreamScope(workstreamId)}{ScopeMaskProject.Replace(ScopeVariableStart + "projectId" + ScopeVariableEnd, projectId)}";

    public static bool IsChildScope(this string scope, string parent)
    {
        if(!scope.EndsWith(ScopeLevelSeperator))
            scope += ScopeLevelSeperator;
        
        if(!parent.EndsWith(ScopeLevelSeperator))
            parent += ScopeLevelSeperator;
        
        return scope.StartsWith(parent, StringComparison.InvariantCultureIgnoreCase);
    }

    [GeneratedRegex(@"\{[a-zA-Z0-9]+\}")]
    private static partial Regex ScopeRegex();
}