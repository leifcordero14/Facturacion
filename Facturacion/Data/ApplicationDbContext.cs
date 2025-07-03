using Facturacion.Models;
using Facturacion.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Facturacion.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Article> Article { get; set; }
    public DbSet<Seller> Seller { get; set; }
    public DbSet<Client> Client { get; set; }
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

      var identificationNumberConverter = new ValueConverter<string, string>(v => CleanIdentificationNumber(v), v => v);

      modelBuilder.Entity<Client>(entity =>
      {
        entity.Property(c => c.IsActive)
              .HasDefaultValue(true);
        entity.Property(c => c.IdentificationNumber)
              .HasConversion(identificationNumberConverter);
      });
    }
    private static string CleanIdentificationNumber(string identificationNumber)
    {
      return identificationNumber.Replace("-", "").Replace(" ", "").Trim();
    }
  }
}
