namespace CondominiumManager.Identity.Application.Contracts.Responses;

internal record UserResponse(Guid Id, string FirstName, string LastName, string Email, string status);
