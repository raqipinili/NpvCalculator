using Microsoft.EntityFrameworkCore;
using Security.Core.Classes;
using Security.Data;
using Security.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Core.Services
{
    public class UserService : IUserService
    {
        private readonly SecurityDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(SecurityDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> AddUser(Register register)
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

            _passwordHasher.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.UserId;
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

            _passwordHasher.CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

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
