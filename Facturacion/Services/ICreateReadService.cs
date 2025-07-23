namespace Facturacion.Services
{
  public interface ICreateReadService<TEntity, TCreateEntity>
  {
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetById(int id);
    Task<TEntity> Create(TCreateEntity createEntity);
  }
}
