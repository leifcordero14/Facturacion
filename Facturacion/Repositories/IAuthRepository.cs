using Facturacion.Models;

namespace Facturacion.Repositories
{
  public interface IAuthRepository
  {
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task Create(User user);
    void Update(User user);
    Task Save();
  }
}
