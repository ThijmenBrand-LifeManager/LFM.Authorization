using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record ListAllRolesQuery() : IRequest<IEnumerable<LfmRole>>;

public class ListAllRolesQueryHandler(IRoleRepository roleRepository) : IRequestHandler<ListAllRolesQuery, IEnumerable<LfmRole>>
{
    public Task<IEnumerable<LfmRole>> Handle(ListAllRolesQuery request, CancellationToken cancellationToken)
    {
        return roleRepository.ListAsync(cancellationToken);
    }
}