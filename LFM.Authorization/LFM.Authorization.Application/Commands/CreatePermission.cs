using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Commands;

public record CreatePermissionCommand(string Name, string Category, string? Description) : IRequest<Permission>;

public class CreatePermissionCommandHandler(IPermissionRepository permissionRepository) : IRequestHandler<CreatePermissionCommand, Permission>
{
    public async Task<Permission> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permission
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description
        };

        var result = await permissionRepository.CreateAsync(permission, cancellationToken);
        return result;
    }
}