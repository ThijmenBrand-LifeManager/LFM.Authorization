using LFM.Authorization.Core.Models;

namespace LFM.Authorization.Application.AuthHelpers;

public interface ITokenService
{
    string CreateToken(LfmUser user);
}