using Facturacion.Data;
using Facturacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Repositories
{
  public class ArticleRepository(ApplicationDbContext context) : IRepository<Article>
  {
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Article>> GetAll()
    {
      return await _context.Article.ToListAsync();
    }
    public async Task<Article?> GetById(int id)
    {
      return await _context.Article.FindAsync(id);
    }
    public async Task Create(Article article)
    {
      await _context.Article.AddAsync(article);
      return;
    }
    public void Update(Article article)
    {
      _context.Article.Update(article);
      return;
    }
    public void Delete(Article article)
    {
      _context.Article.Remove(article);
      return;
    }
    public async Task Save()
    {
      await _context.SaveChangesAsync();
      return;
    }
  }
}
