using Facturacion.Data;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Utilities
{
  public class EntityExistenceChecker(ApplicationDbContext context)
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> ArticleExists(int id, CancellationToken ct = default)
    {
      return await _context.Set<Article>().AnyAsync(e => e.Id == id, ct);
    }

    public async Task<bool> ClientExists(int id, CancellationToken ct = default)
    {
      return await _context.Set<Client>().AnyAsync(e => e.Id == id, ct);
    }

    public async Task<bool> SellerExists(int id, CancellationToken ct = default)
    {
      return await _context.Set<Seller>().AnyAsync(e => e.Id == id, ct);
    }
  }
}
