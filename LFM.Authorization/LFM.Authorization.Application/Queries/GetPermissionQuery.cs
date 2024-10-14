using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record GetPermissionQuery(string Name) : IRequest<Permission>;

public class GetPermissionQueryHandler(IPermissionRepository permissionRepository) : IRequestHandler<GetPermissionQuery, Permission>
{
    public async Task<Permission> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetByIdAsync(request.Name, cancellationToken);
        if (permission == null)
            throw new NullReferenceException(nameof(Permission));
        
        return permission;
    }
}