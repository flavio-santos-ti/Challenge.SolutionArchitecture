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
            .HasKey(t => t.Id); 

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(14, 2)
            .HasColumnName("amount");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasMaxLength(10)
            .HasColumnName("type");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Description)
            .HasMaxLength(255)
            .HasColumnName("description");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.OccurredAt)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v))
            .HasColumnName("occurred_at")
            .HasColumnType("timestamp");

        modelBuilder.Entity<Transaction>()
            .Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp");
    }
}
