using Microsoft.EntityFrameworkCore;
using NpvCalculator.Data;
using NpvCalculator.Data.Entities;
using NpvCalculator.Security.Classes;
using NpvCalculator.Security.Helpers;
using System;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName);

            if (user == null)
                throw new UnauthorizedAccessException("Unauthorized Access");

            if (!AuthHelper.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Unauthorized Access");

            Claim[] claims = AuthHelper.GenerateClaims(user);
            return AuthHelper.GenerateTokenV2(_jwtOptions, claims);
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.UserId;
        }
    }
}
