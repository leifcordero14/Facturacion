using Facturacion.Models;

namespace Facturacion.Repositories
{
  public interface IAuthRepository
  {
    Task<User?> GetByEmail(string email);
    Task Create(User user);
    Task Save();
  }
}
