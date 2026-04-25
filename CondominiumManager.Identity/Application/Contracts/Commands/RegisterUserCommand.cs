namespace CondominiumManager.Identity.Application.Contracts.Commands;

internal record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password);

