using CondominiumManager.Identity.Application;
using CondominiumManager.Identity.Application.Contracts.Responses;
using FastEndpoints;

namespace CondominiumManager.Identity.UserEndPoints;

internal class Register(RegisterUserHandler registerUserHandler) : Endpoint<UserRequest,UserResponse>
{
    private readonly RegisterUserHandler _registerUserHandler = registerUserHandler;

    public override void Configure()
    {
        Post("/user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserRequest userRequest, CancellationToken ct)
    {
       
        var result = await _registerUserHandler.HandleAsync(userRequest, ct);

        if (result.IsFailure)
        {
            await Send.ErrorsAsync();
            return;

        }
            

        await Send.OkAsync(result.Value);
    }
}
