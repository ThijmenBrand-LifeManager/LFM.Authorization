using LFM.Authorization.AspNetCore.Models;
using LFM.Authorization.Repository.Interfaces;

namespace LFM.Authorization.Repository.Repository;

public class PermissionRepository(DatabaseContext context) : RepositoryBase<Permission>(context), IPermissionRepository
{
}