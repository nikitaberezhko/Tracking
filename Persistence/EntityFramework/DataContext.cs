using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>()
            .Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Status>()
            .Property(x => x.OrderId).HasColumnName("order_id");
        modelBuilder.Entity<Status>()
            .Property(x => x.CompletionPercent).HasColumnName("completion_percent");
        modelBuilder.Entity<Status>()
            .Property(x => x.StatusType).HasColumnName("status_type");
        
        modelBuilder
            .Entity<Status>()
            .HasIndex(x => x.OrderId)
            .IsUnique();
    }
}