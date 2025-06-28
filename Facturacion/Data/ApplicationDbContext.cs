using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Article> Article { get; set; }
    public DbSet<Seller> Seller { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Article>(entity =>
      {
        entity.Property(a => a.IsAvailable)
              .HasDefaultValue(true);
        entity.Property(a => a.UnitPrice)
              .HasPrecision(18, 2);
      });

      modelBuilder.Entity<Seller>(entity =>
      { 
        entity.Property(s => s.IsActive)
              .HasDefaultValue(true);
        entity.Property(s => s.CommissionPercentage)
              .HasDefaultValue(0);
        entity.ToTable(s => s.HasCheckConstraint("CK_Prices", "[CommissionPercentage] BETWEEN 0 AND 100"));
      });
    }
  }
}
