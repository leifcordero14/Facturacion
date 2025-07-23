using Facturacion.Data;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Repositories
{
  public class BillingRepository(ApplicationDbContext context) : IGetPostRepository<Billing>
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Billing>> GetAll()
    {
      return await _context.Billing
        .Include(b => b.Article)
        .Include(b => b.Client)
        .Include(b => b.Seller)
        .ToListAsync();
    }
    public async Task<Billing?> GetById(int id)
    {
      return await _context.Billing
        .Include(b => b.Article)
        .Include(b => b.Client)
        .Include(b => b.Seller)
        .FirstOrDefaultAsync(b => b.Id == id);
    }
    public async Task Create(Billing billing)
    {
      await _context.Billing.AddAsync(billing);
      return;
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
      return;
    }
  }
}
