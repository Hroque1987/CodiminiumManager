namespace Sharedkernel.Abstractions;

public interface ICurrentUser
{
    Guid Id { get; }
    bool IsAuthenticated { get; }
    string? Email { get; }
}
