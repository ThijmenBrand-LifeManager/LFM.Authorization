using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Commands;

public record CreateRoleCommand(string Name, string Scope, string? Description, bool ignoreIfExists = false) : IRequest<LfmRole>;

public class CreateRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<CreateRoleCommand, LfmRole>
{
    public Task<LfmRole> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new LfmRole()
        {
            Name = request.Name,
            Scope = request.Scope,
            Description = request.Description
        };
        
        if (request.ignoreIfExists)
        {
            return roleRepository.CreateAsync(role, cancellationToken);
        }

        return roleRepository.CreateAsync(role, cancellationToken);
    }
}