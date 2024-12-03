namespace MetaCortex.Customers.DataAccess.Interfaces;

public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
{
    Task<TEntity> GetByIdAsync(TId id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
}