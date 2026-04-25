using CondominiumManager.Identity.Application.Contracts.Responses;
using CondominiumManager.Identity.Domain.Entities;

namespace CondominiumManager.Identity.Application.Mapping;

internal static class UserMappings
{
    public static UserResponse ToResponse(this User user)
    {
        var response = new UserResponse(
                        user.Id,
                        user.Name.FirstName,
                        user.Name.LastName,
                        user.Email.Value,
                        user.Status.ToString()
                        );

        return response;
    }
  
    
}
