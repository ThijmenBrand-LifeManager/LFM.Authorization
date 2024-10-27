using LFM.Authorization.Application.Commands;
using LFM.Authorization.Application.Queries;
using LFM.Authorization.AspNetCore.Services;
using LFM.WorkStream.Core.Messages.Events;
using MassTransit;
using MediatR;

namespace LFM.Authorization.Endpoint.Consumers;

public class WorkstreamCreatedConsumer(ISender sender) : IConsumer<WorkstreamCreatedEvent>
{
    public async Task Consume(ConsumeContext<WorkstreamCreatedEvent> context)
    {
        var defaultRolePermissions = await sender.Send(new ListDefaultRolePermissionsQuery());
        var roleScope = ScopeHelper.CreateWorkstreamScope(context.Message.WorkstreamId);
        const string roleDescription = "Auto-generated role";

        var rolePermissions = defaultRolePermissions.ToList();
        rolePermissions = rolePermissions.GroupBy(x => x.Role).Select(x => x.First()).ToList();
    }
}