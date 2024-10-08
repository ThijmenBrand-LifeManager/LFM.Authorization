using LFM.Authorization.Core.Models;
using LFM.Authorization.Repository.Interfaces;

namespace LFM.Authorization.Repository.Repository;

public class PermissionRepository(DatabaseContext context) : RepositoryBase<Permission>(context), IPermissionRepository
{
}