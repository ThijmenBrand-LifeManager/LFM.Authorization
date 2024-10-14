using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record ListPermissionsQuery : IRequest<IEnumerable<Permission>>;

public class ListPermissionsQueryHandler(IPermissionRepository permissionRepository) : IRequestHandler<ListPermissionsQuery, IEnumerable<Permission>>
{
    public Task<IEnumerable<Permission>> Handle(ListPermissionsQuery request, CancellationToken cancellationToken)
    {
        return permissionRepository.ListAsync(cancellationToken);
    }
}