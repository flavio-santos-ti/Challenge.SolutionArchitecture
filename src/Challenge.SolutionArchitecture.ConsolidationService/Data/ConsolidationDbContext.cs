using Challenge.SolutionArchitecture.ConsolidationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.SolutionArchitecture.ConsolidationService.Data;

public class ConsolidationDbContext : DbContext
{
    public ConsolidationDbContext(DbContextOptions<ConsolidationDbContext> options)
        : base(options) { }

    public DbSet<DailyBalance> DailyBalances => Set<DailyBalance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DailyBalance>().ToTable("daily_balances");

        modelBuilder.Entity<DailyBalance>()
            .Property(b => b.ReferenceDate)
            .HasColumnName("reference_date")
            .HasColumnType("date");

        modelBuilder.Entity<DailyBalance>()
            .Property(b => b.TotalCredit)
            .HasColumnName("total_credit")
            .HasPrecision(14, 2);

        modelBuilder.Entity<DailyBalance>()
            .Property(b => b.TotalDebit)
            .HasColumnName("total_debit")
            .HasPrecision(14, 2);

        modelBuilder.Entity<DailyBalance>()
            .Property(b => b.Balance)
            .HasColumnName("balance")
            .HasPrecision(14, 2);

        modelBuilder.Entity<DailyBalance>()
            .Property(b => b.GeneratedAt)
            .HasColumnName("generated_at")
            .HasColumnType("timestamp with time zone");
    }
}
