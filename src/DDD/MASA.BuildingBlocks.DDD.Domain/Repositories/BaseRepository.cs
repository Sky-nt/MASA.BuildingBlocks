﻿namespace MASA.BuildingBlocks.DDD.Domain.Repositories;
public abstract class BaseRepository<TEntity> : IRepository<TEntity>, IUnitOfWork
    where TEntity : class, IAggregateRoot
{
    #region IRepository<TEntity>

    public abstract ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await AddAsync(entity, cancellationToken);
        }
    }

    public virtual async ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
    {
        return await FindAsync(keyValues, default);
    }

    public abstract ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken);

    public abstract Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    public abstract Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    public abstract Task RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await RemoveAsync(entity, cancellationToken);
        }
    }

    public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await UpdateAsync(entity, cancellationToken);
        }
    }

    public abstract Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken);

    public abstract Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    public abstract Task<long> GetCountAsync(CancellationToken cancellationToken);

    public abstract Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    public abstract Task<List<TEntity>> GetPaginatedListAsync(int skip, int take, string? sorting, CancellationToken cancellationToken);

    public abstract Task<List<TEntity>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, string? sorting, CancellationToken cancellationToken);

    public virtual async Task<PaginatedList<TEntity>> GetPaginatedListAsync(PaginatedOptions options, CancellationToken cancellationToken)
    {
        var result = await GetPaginatedListAsync(
            (options.Page - 1) * options.PageSize,
            options.PageSize <= 0 ? int.MaxValue : options.PageSize,
            options.Sorting,
            cancellationToken
        );

        var total = await GetCountAsync(cancellationToken);

        return new PaginatedList<TEntity>()
        {
            Total = total,
            Result = result,
            TotalPages = (int)Math.Ceiling(total / (decimal)options.PageSize)
        };
    }

    public async Task<PaginatedList<TEntity>> GetPaginatedListAsync(Expression<Func<TEntity, bool>> predicate, PaginatedOptions options, CancellationToken cancellationToken)
    {
        var result = await GetPaginatedListAsync(
            predicate,
            (options.Page - 1) * options.PageSize,
            options.PageSize <= 0 ? int.MaxValue : options.PageSize,
            options.Sorting,
            cancellationToken
        );

        var total = await GetCountAsync(predicate, cancellationToken);

        return new PaginatedList<TEntity>()
        {
            Total = total,
            Result = result,
            TotalPages = (int)Math.Ceiling(total / (decimal)options.PageSize)
        };
    }

    #endregion

    #region IUnitOfWork

    public bool DisableRollbackOnFailure { get; set; }

    public abstract DbTransaction Transaction { get; }

    public abstract IUnitOfWork UnitOfWork { get; }

    public abstract bool TransactionHasBegun { get; }

    public abstract Task CommitAsync(CancellationToken cancellationToken = default);

    public abstract ValueTask DisposeAsync();

    public abstract Task RollbackAsync(CancellationToken cancellationToken = default);

    public abstract Task SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion
}