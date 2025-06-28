using Facturacion.DTOs;

namespace Facturacion.Services
{
  public interface IService<TEntity, TCreateEntity, TUpdateEntity>
  {
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> GetById(int id);
    Task<TEntity> Create(TCreateEntity createEntity);
    Task Update(int id, TUpdateEntity updateEntity);
    Task Delete(int id);
  }
}
