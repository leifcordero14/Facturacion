using Facturacion.Data;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Repositories
{
  public class ClientRepository(ApplicationDbContext context) : IRepository<Client>
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Client>> GetAll()
    {
      return await _context.Client.ToListAsync();
    }
    public async Task<Client?> GetById(int id)
    {
      return await _context.Client.FindAsync(id);
    }
    public async Task Create(Client client)
    {
      await _context.Client.AddAsync(client);
      return;
    }
    public void Update(Client client)
    {
      _context.Client.Update(client);
      return;
    }
    public void Delete(Client client)
    {
      _context.Client.Remove(client);
      return;
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
      return;
    }
  }
}
