using System.Collections;
using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record ListDefaultRolePermissionsQuery : IRequest<IEnumerable<DefaultRolePermission>>;

public class ListDefaultRolePermissionsQueryHandler(IDefaultRolePermissionRepository defaultRolePermissionRepository)
    : IRequestHandler<ListDefaultRolePermissionsQuery, IEnumerable<DefaultRolePermission>>
{
    public async Task<IEnumerable<DefaultRolePermission>> Handle(ListDefaultRolePermissionsQuery request, CancellationToken cancellationToken) {
        return await defaultRolePermissionRepository.ListAsync(cancellationToken);
    }

}