namespace Facturacion.Services
{
  public interface IFilterService<TEntity, TEntityFilter>
  {
    Task<IEnumerable<TEntity>> GetByFilter(TEntityFilter filter);
  }
}
