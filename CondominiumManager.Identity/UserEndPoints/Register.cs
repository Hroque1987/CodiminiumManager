using CondominiumManager.Identity.Application;
using CondominiumManager.Identity.Application.Commands;
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

    public override async Task HandleAsync(UserRequest req, CancellationToken ct)
    {
        RegisterUserCommand registerUserCommand = new (Guid.NewGuid(), req.FirstName, req.LastName, req.Email);
        var result = await _registerUserHandler.HandleAsync(registerUserCommand);

        if (result.IsFailure)
        {
            await Send.ErrorsAsync();
            return;

        }
            

        await Send.OkAsync(result.Value);
    }
}
