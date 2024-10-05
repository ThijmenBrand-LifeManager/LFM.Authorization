using LFM.Authorization.Core.Models;

namespace LFM.Authorization.Core.AuthHelpers;

public interface ITokenService
{
    string CreateToken(LfmUser user);
}