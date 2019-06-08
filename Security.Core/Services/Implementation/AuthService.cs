using Microsoft.EntityFrameworkCore;
using Security.Data;
using Security.Core.Classes;
using Security.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly SecurityDbContext _context;
        private readonly JwtOptions _jwtOptions;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            SecurityDbContext context,
            JwtOptions jwtOptions,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _context = context;
            _jwtOptions = jwtOptions;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> Login(Login login)
        {
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                .FirstOrDefaultAsync(u => u.UserName == login.UserName);

            if (user == null)
                throw new UnauthorizedAccessException("Unauthorized Access");

            if (!_passwordHasher.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Unauthorized Access");

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString(), ClaimValueTypes.String));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName, ClaimValueTypes.String));

            // when a user has only one permission, 'permissions claim' will not be an array,
            // add a dummy value to make the 'permissions claim' an array if user has only one permission
            claims.Add(new Claim("permissions", "0", ClaimValueTypes.Integer));

            var userPermissions = await _context.UserPermissions
                .Where(up => up.UserId == user.UserId)
                .Include(up => up.Permission)
                .Select(up => new Claim("permissions", up.Permission.PermissionId.ToString(), ClaimValueTypes.Integer))
                .ToListAsync();

            claims.AddRange(userPermissions);

            return _tokenService.GenerateTokenV2(claims.ToArray(), _jwtOptions);
        }
    }
}
