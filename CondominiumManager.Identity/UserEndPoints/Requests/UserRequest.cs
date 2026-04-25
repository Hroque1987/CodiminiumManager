namespace CondominiumManager.Identity.UserEndPoints;

internal record UserRequest(string FirstName, string LastName, string Email, string Password);