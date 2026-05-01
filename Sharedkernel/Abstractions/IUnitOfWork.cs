namespace Sharedkernel.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
}
