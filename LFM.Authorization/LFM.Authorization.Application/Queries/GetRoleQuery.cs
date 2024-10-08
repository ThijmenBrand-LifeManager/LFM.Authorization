using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record GetRoleQuery(string RoleId) : IRequest<LfmRole>;

public class GetRoleQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleQuery, LfmRole>
{
    public async Task<LfmRole> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if(role == null) 
            throw new Exception($"role with {request.RoleId} not found");
        
        return role;
    }
}