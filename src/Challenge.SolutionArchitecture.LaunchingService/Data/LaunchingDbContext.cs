using Microsoft.EntityFrameworkCore;
using Challenge.SolutionArchitecture.LaunchingService.Models;

namespace Challenge.SolutionArchitecture.LaunchingService.Data;

public class LaunchingDbContext : DbContext
{
    public LaunchingDbContext(DbContextOptions<LaunchingDbContext> options)
    : base(options) { }

    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().ToTable("transactions");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(14, 2);

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasMaxLength(10);
    }
}
