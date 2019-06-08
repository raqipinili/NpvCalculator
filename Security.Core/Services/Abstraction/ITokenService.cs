using Security.Core.Helpers;
using System.Security.Claims;

namespace Security.Core.Services
{
    public interface ITokenService
    {
        string GenerateTokenV1(Claim[] claims, JwtOptions jwtOptions);
        string GenerateTokenV2(Claim[] claims, JwtOptions jwtOptions);
    }
}
