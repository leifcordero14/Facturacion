using Facturacion.Data;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Repositories
{
  public class SellerRepository(ApplicationDbContext context) : IRepository<Seller>
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Seller>> GetAll()
    {
      return await _context.Seller.ToListAsync();
    }
    public async Task<Seller?> GetById(int id)
    {
      return await _context.Seller.FindAsync(id);
    }
    public async Task Create(Seller seller)
    {
      await _context.Seller.AddAsync(seller);
      return;
    }
    public void Update(Seller seller)
    {
      _context.Seller.Update(seller);
      return;
    }
    public void Delete(Seller seller)
    {
      _context.Seller.Remove(seller);
      return;
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
      return;
    }
  }
}
