namespace MASA.BuildingBlocks.Data.UoW;
public interface IUnitOfWork : IAsyncDisposable
{
    DbTransaction Transaction { get; }

    bool TransactionHasBegun { get; }

    bool UseTransaction { get; set; }

    bool DisableRollbackOnFailure { get; set; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
