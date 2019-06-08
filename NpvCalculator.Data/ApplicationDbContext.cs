using Microsoft.EntityFrameworkCore;
using NpvCalculator.Data.Entities;

namespace NpvCalculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NetPresentValue>().ToTable(name: "NetPresentValues", schema: "Calculator");
            modelBuilder.Entity<PeriodAmount>().ToTable(name: "PeriodAmounts", schema: "Calculator");
            modelBuilder.Entity<UserNetPresentValue>().ToTable(name: "UserNetPresentValues", schema: "Calculator");
            modelBuilder.Entity<CalculatorType>().ToTable(name: "CalculatorTypes", schema: "Lookup");
        }

        // Calculator
        public DbSet<NetPresentValue> NetPresentValues { get; set; }
        public DbSet<PeriodAmount> PeriodAmounts { get; set; }
        public DbSet<UserNetPresentValue> UserNetPresentValues { get; set; }

        // Lookup
        public DbSet<CalculatorType> CalculatorTypes { get; set; }
    }
}
