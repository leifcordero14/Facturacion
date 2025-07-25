using Facturacion.Data;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Repositories
{
  public class AuthRepository(ApplicationDbContext context) : IAuthRepository
  {
    private readonly ApplicationDbContext _context = context;
    public async Task<User?> GetById(int id)
    {
      return await _context.User.FindAsync(id);
    }
    public async Task<User?> GetByEmail(string email)
    {
      return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task Create(User user)
    {
      await _context.User.AddAsync(user);
      return;
    }
    public void Update(User user)
    {
      _context.User.Update(user);
      return;
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
      return;
    }
  }
}
