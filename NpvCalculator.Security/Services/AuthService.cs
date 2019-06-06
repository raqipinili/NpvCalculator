using Microsoft.EntityFrameworkCore;
using NpvCalculator.Data;
using NpvCalculator.Data.Entities;
using NpvCalculator.Security.Classes;
using NpvCalculator.Security.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NpvCalculator.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly CalculatorDbContext _context;
        private readonly JwtOptions _jwtOptions;

        public AuthService(CalculatorDbContext context, JwtOptions jwtOptions)
        {
            _context = context;
            _jwtOptions = jwtOptions;
        }

        public async Task<string> Login(Login login)
        {
            try
            {
                var user = await _context.Users.Include(u => u.UserPermissions).FirstOrDefaultAsync(u => u.UserName == login.UserName);

                if (user == null)
                    throw new UnauthorizedAccessException("Unauthorized Access");

                if (!AuthHelper.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
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

                return AuthHelper.GenerateTokenV2(_jwtOptions, claims.ToArray());
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw;
            }
        }

        public async Task<Guid> Register(Register register)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == register.UserName))
                throw new Exception("Username already exists");

            var user = new User()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName.ToLower(),
                EmailAddress = register.EmailAddress,
                CreatedDate = DateTime.UtcNow
            };

            AuthHelper.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            /* https://devblogs.microsoft.com/cesardelatorre/using-resilient-entity-framework-core-sql-connections-and-transactions-retries-with-exponential-backoff/ */
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    int saveCount = 0;

                    await _context.Users.AddAsync(user);
                    saveCount = await _context.SaveChangesAsync();

                    if (register.Permissions != null && register.Permissions.Count() > 0)
                    {
                        var userPermissions = register.Permissions.Select(permissionId => new UserPermission()
                        {
                            UserId = user.UserId,
                            PermissionId = permissionId
                        });

                        await _context.UserPermissions.AddRangeAsync(userPermissions);
                        saveCount = await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
            });

            return user.UserId;
        }
    }
}
