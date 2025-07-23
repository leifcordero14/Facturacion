using Facturacion.Data;
using Facturacion.DTOs;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Repositories
{
  public class BillingRepository(ApplicationDbContext context) : IGetPostRepository<Billing>, IFilterRepository<Billing, BillingFilterDto>
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
    public async Task<IEnumerable<Billing>> GetByFilter(BillingFilterDto filter)
    {
      var query = _context.Billing
        .Include(b => b.Article)
        .Include(b => b.Client)
        .Include(b => b.Seller)
        .AsQueryable();

      if (filter.ArticleId.HasValue) query = query.Where(b => b.ArticleId == filter.ArticleId.Value);
      if (filter.SellerId.HasValue) query = query.Where(b => b.SellerId == filter.SellerId.Value);
      if (filter.ClientId.HasValue) query = query.Where(b => b.ClientId == filter.ClientId.Value);
      if (filter.StartDate.HasValue) query = query.Where(b => b.CreatedAt>= filter.StartDate.Value);
      if (filter.EndDate.HasValue) query = query.Where(b => b.CreatedAt <= filter.EndDate.Value);

      return await query.ToListAsync();
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
