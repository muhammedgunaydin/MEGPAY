using System.Linq.Expressions;
using Transfer.Shared.Entities;

namespace Transfer.Domain.SeedWork;

public interface IGenericRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);

    public Task<TEntity?> GetByIdWithIncludesAsync(
        TId id,
        Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includes);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> FilterByAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TId> SaveAsync(TEntity entity, CancellationToken cancellationToken);
    TEntity Update(TEntity entity);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity Remove(TEntity entity);
}