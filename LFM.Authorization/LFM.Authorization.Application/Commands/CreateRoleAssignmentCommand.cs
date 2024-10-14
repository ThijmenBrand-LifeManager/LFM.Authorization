using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Commands;

public record CreateRoleAssignmentCommand(string UserId, string RoleId, string Scope) : IRequest<RoleAssignment>;

public class CreateRoleAssignmentCommandHandler(IRoleRepository roleRepository, 
    IRoleAssignmentRepository roleAssignmentRepository) : IRequestHandler<CreateRoleAssignmentCommand, RoleAssignment>
{
    public async Task<RoleAssignment> Handle(CreateRoleAssignmentCommand request, CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role == null) throw new Exception("No role exists for this scope");
        
        var roleAssignment = new RoleAssignment
        {
            UserId = request.UserId,
            Scope = request.Scope,
            RoleId = role.Id,
        };

        return await roleAssignmentRepository.CreateAsync(roleAssignment, cancellationToken);
    }
}