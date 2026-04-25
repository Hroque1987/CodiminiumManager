namespace CondominiumManager.Identity.Application.Commands;

internal record RegisterUserCommand(Guid Id, string FirstName, string LastName, string Email);