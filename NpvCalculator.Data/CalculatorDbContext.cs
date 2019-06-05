using Microsoft.EntityFrameworkCore;
using NpvCalculator.Data.Entities;

namespace NpvCalculator.Data
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(name: "Users", schema: "Security");
        }

        // Calculator
        public DbSet<NetPresentValue> NetPresentValues { get; set; }

        // Lookup
        public DbSet<CalculatorType> CalculatorTypes { get; set; }

        // Security
        public DbSet<User> Users { get; set; }
    }
}
