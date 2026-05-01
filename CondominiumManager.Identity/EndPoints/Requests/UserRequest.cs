namespace CondominiumManager.Identity.UserEndPoints.Requests;

internal record UserRequest(string FirstName, string LastName, string Email, string Password);