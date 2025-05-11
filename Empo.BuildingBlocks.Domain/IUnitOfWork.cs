namespace Empo.BuildingBlocks.Domain
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
