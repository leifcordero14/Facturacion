namespace Facturacion.Repositories
{
  public interface IRepository<TEntity>
  {
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Save();
  }
}
