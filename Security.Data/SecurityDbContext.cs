using Microsoft.EntityFrameworkCore;
using Security.Data.Entities;

namespace Security.Data
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options) { }

        // https://github.com/aspnet/EntityFrameworkCore/issues/7533
        protected SecurityDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(name: "Users", schema: "Security");
            modelBuilder.Entity<UserPermission>().ToTable(name: "UserPermissions", schema: "Security");
            modelBuilder.Entity<Permission>().ToTable(name: "Permissions", schema: "Security");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
