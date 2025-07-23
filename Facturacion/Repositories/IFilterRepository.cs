namespace Facturacion.Repositories
{
  public interface IFilterRepository<TEntity, TEntityFilter>
  {
    Task<IEnumerable<TEntity>> GetByFilter(TEntityFilter filter);
  }
}
