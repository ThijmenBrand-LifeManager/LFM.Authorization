using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;
using MediatR;

namespace LFM.Authorization.Application.Queries;

public record ListRolesQuery(string Scope) : IRequest<IEnumerable<LfmRole>>;

public class ListRolesQueryHandler(IRoleRepository roleRepository) : IRequestHandler<ListRolesQuery, IEnumerable<LfmRole>>
{
    public Task<IEnumerable<LfmRole>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
    {
        return roleRepository.ListByScopeAsync(request.Scope, cancellationToken);
    }
}