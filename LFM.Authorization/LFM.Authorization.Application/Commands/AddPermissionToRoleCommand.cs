using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Commands;

public record AddPermissionToRoleCommand(string RoleName, string RoleScope, string PermissionName) : IRequest<LfmRole>;

public class AddPermissionToRoleCommandHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository) : IRequestHandler<AddPermissionToRoleCommand, LfmRole>
{
    public async Task<LfmRole> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByNameAndScopeAsync(request.RoleName, request.RoleScope, cancellationToken) 
                   ?? throw new Exception("Role not found!");
        var permission = await permissionRepository.GetByIdAsync(request.PermissionName, cancellationToken) 
                         ?? throw new Exception("Permission not found!");
        
        if (role.Permissions.All(p => p.Name != request.PermissionName))
            role.Permissions.Add(permission);
        
        await roleRepository.UpdateAsync(cancellationToken);

        return role;
    }
}