namespace Facturacion.Repositories
{
  public interface IGetPostRepository<TEntity>
  {
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetById(int id);
    Task Create(TEntity entity);
    Task Save();
  }
}
