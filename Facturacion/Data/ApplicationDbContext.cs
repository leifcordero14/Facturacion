using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Article> Article { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Article>()
          .Property(a => a.IsAvailable)
          .HasDefaultValue(true);
    }
  }
}
