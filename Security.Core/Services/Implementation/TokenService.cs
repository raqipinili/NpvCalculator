using Microsoft.IdentityModel.Tokens;
using Security.Core.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Security.Core.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateTokenV1(Claim[] claims, JwtOptions jwtOptions)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                NotBefore = jwtOptions.NotBefore,
                Expires = jwtOptions.Expiration,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = jwtOptions.SigningCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateTokenV2(Claim[] claims, JwtOptions jwtOptions)
        {
            var jwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                notBefore: jwtOptions.NotBefore,
                expires: jwtOptions.Expiration,
                claims: claims,
                signingCredentials: jwtOptions.SigningCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }
    }
}
