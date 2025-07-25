using Facturacion.Data;
using Facturacion.Models;

namespace Facturacion.Repositories
{
  public class AuthRepository(ApplicationDbContext context) : IAuthRepository
  {
    private readonly ApplicationDbContext _context = context;
    public async Task<User?> GetByEmail(string email)
    {
      return await _context.User.FindAsync(email);
    }
    public async Task Create(User user)
    {
      await _context.User.AddAsync(user);
      return;
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
      return;
    }
  }
}
