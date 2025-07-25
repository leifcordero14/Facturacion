using Facturacion.Data;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Utilities
{
  public class EntityExistenceChecker(ApplicationDbContext context)
  {
    private readonly ApplicationDbContext _context = context;
    public async Task<bool> ArticleExists(int id, CancellationToken ct = default)
    {
      return await _context.Article.AnyAsync(a => a.Id == id, ct);
    }
    public async Task<bool> ClientExists(int id, CancellationToken ct = default)
    {
      return await _context.Client.AnyAsync(c => c.Id == id, ct);
    }
    public async Task<bool> SellerExists(int id, CancellationToken ct = default)
    {
      return await _context.Seller.AnyAsync(s => s.Id == id, ct);
    }
    public async Task<bool> UserExists(string email, CancellationToken ct = default)
    {
      return await _context.User.AnyAsync(u => u.Email == email, ct);
    }
  }
}
