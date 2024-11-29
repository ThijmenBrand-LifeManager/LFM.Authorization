using LFM.Authorization.Application.Commands;
using LFM.Authorization.Application.Queries;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.AspNetCore.Services;
using LFM.WorkStream.Core.Messages.Events;
using MassTransit;
using MediatR;

namespace LFM.Authorization.Endpoint.Consumers;

public class WorkstreamCreatedConsumer(ISender sender) : IConsumer<WorkstreamCreatedEvent>
{
    public async Task Consume(ConsumeContext<WorkstreamCreatedEvent> context)
    {
        //TODO split logic into modules. Split into a module which handles the role and permission creation, and a role assignment module
        var defaultRolePermissions = await sender.Send(new ListDefaultRolePermissionsQuery());
        var roleScope = ScopeHelper.CreateWorkstreamScope(context.Message.WorkstreamId);
        const string roleDescription = "Auto-generated role";

        var rolePermissions = defaultRolePermissions.ToList();
        foreach (var rolePermission in rolePermissions)
        {
            var role = await sender.Send(new CreateRoleCommand(rolePermission.Role, roleScope, roleDescription, true));
            await sender.Send(new AddPermissionToRoleCommand(role.Name, role.Scope, rolePermission.PermissionName));
        }
        
        var userId = context.Message.CreatorId;
        var roleAssignment =
            await sender.Send(new CreateRoleAssignmentCommand(userId, DefaultRoles.ProjectAdmin.ToString(), roleScope));
    }
}