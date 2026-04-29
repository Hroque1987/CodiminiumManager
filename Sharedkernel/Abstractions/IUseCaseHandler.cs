
using Sharedkernel.Results;

namespace Sharedkernel.Abstractions;

public interface IUseCaseHandler<TRequest, TResult>
{
    Task<Result<TResult>> HandleAsync(TRequest request, CancellationToken ct);
}