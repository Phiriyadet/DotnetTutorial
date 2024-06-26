using System.Linq.Expressions;
using Domain;

namespace Core;

public interface IBaseRepository<TEntity> where TEntity:IBaseEntity
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<bool> DoesExist(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);
    Task SaveChangesAsync();
}