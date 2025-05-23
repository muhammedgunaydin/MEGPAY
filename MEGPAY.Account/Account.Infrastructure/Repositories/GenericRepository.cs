using System.Linq.Expressions;
using Account.Domain.SeedWork;
using Account.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Repositories;

public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : Entity<TId>
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.Where(predicate).CountAsync();
    }

    public IQueryable<TEntity> FilterByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Where(predicate);
    }

    public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.SingleOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await _entities.SingleOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
    }

    public async Task<TEntity?> GetByIdWithIncludesAsync(
        TId id,
        Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _entities;

        if (includes != null && includes.Length > 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        // Eğer bir predicate verilmişse bu koşul uygulanır
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.SingleOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
    }

    public TEntity Remove(TEntity entity)
    {
        return _entities.Remove(entity).Entity;
    }

    public async Task<TId> SaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var entityEntry = await _entities.AddAsync(entity, cancellationToken);
        var id = entityEntry.Entity.Id;
        return id;
    }

    public async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.Where(predicate).ToListAsync();
    }

    public TEntity Update(TEntity entity)
    {
        var entityEntry = _entities.Update(entity);
        return entityEntry.Entity;
    }
}