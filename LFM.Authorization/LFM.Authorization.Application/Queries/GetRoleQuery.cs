using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record GetRoleQuery(string RoleName, string Scope) : IRequest<LfmRole>;

public class GetRoleQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleQuery, LfmRole>
{
    public async Task<LfmRole> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByNameAndScopeAsync(request.RoleName, request.Scope, cancellationToken);
        if(role == null) 
            throw new Exception($"role: {request.RoleName} not found");
        
        return role;
    }
}