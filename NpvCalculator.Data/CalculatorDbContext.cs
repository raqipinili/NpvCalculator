using Microsoft.EntityFrameworkCore;
using NpvCalculator.Data.Entities;

namespace NpvCalculator.Data
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NetPresentValue>().ToTable(name: "NetPresentValues", schema: "Calculator");
            modelBuilder.Entity<PeriodAmount>().ToTable(name: "PeriodAmounts", schema: "Calculator");
            

            modelBuilder.Entity<CalculatorType>().ToTable(name: "CalculatorTypes", schema: "Lookup");

            modelBuilder.Entity<User>().ToTable(name: "Users", schema: "Security");
            modelBuilder.Entity<UserNetPresentValue>().ToTable(name: "UserNetPresentValues", schema: "Security");
        }

        // Calculator
        public DbSet<NetPresentValue> NetPresentValues { get; set; }
        public DbSet<PeriodAmount> PeriodAmounts { get; set; }

        // Lookup
        public DbSet<CalculatorType> CalculatorTypes { get; set; }

        // Security
        public DbSet<User> Users { get; set; }
        public DbSet<UserNetPresentValue> UserNetPresentValues { get; set; }
    }
}
