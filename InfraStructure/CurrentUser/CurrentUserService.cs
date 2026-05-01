using Microsoft.AspNetCore.Http;
using Sharedkernel.Abstractions;

namespace InfraStructure.CurrentUser;

public class CurrentUserService : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid Id
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User?.FindFirst("sub")?.Value;

            if (Guid.TryParse(value, out var id))
                return id;

            throw new InvalidOperationException("User ID claim is missing or invalid.");
        }
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public string? Email => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
}
