using Facturacion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Facturacion.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Article> Article { get; set; }
    public DbSet<Seller> Seller { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Billing> Billing { get; set; }
    public DbSet<User> User { get; set; }
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
        entity.HasIndex(c => c.IdentificationNumber)
              .IsUnique();
      });

      modelBuilder.Entity<Billing>(entity =>
      {
        entity.Property(b => b.UnitPrice)
              .HasPrecision(18, 2);
        entity.Property(b => b.CreatedAt)
              .HasDefaultValueSql("GETUTCDATE()");
        entity.Property(b => b.Quantity)
              .HasDefaultValue(1);
        entity.Property(b => b.Comment)
              .HasDefaultValue(string.Empty);
        entity.Ignore(b => b.Amount);
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.HasIndex(u => u.Email)
              .IsUnique();
      });
    }
    private static string CleanIdentificationNumber(string identificationNumber)
    {
      return identificationNumber.Replace("-", "").Replace(" ", "").Trim();
    }
  }
}
