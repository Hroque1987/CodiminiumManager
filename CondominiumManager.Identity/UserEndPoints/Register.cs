using CondominiumManager.Identity.Application.Contracts.Commands;
using CondominiumManager.Identity.Application.UseCases;
using CondominiumManager.Identity.UserEndPoints.Mapping;
using CondominiumManager.Identity.UserEndPoints.Requests;
using CondominiumManager.Identity.UserEndPoints.Responses;
using FastEndpoints;
using InfraStructure.FastEndPoints.PostProcessors;
using InfraStructure.FastEndPoints.PreProcessors;
using Microsoft.AspNetCore.Http;

namespace CondominiumManager.Identity.UserEndPoints;

internal class Register(RegisterUserHandler registerUserHandler) : Endpoint<UserRequest, RegisterUserResponse>
{
    private readonly RegisterUserHandler _registerUserHandler = registerUserHandler;

    public override void Configure()
    {
        Post("/user");
        PreProcessor<LoggingPreProcessor<UserRequest>>();
        PostProcessor<LoggingPostProcessor<UserRequest, RegisterUserResponse>>();
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(UserRequest userRequest, CancellationToken ct)
    {
        var command = new RegisterUserCommand(
                    userRequest.FirstName,
                    userRequest.LastName,
                    userRequest.Email,
                    userRequest.Password);


        var result = await _registerUserHandler.HandleAsync(command, ct);

        if (result.ToHttpError() is IResult error)
        {
            await Send.ResultAsync(error);
            return;
        }

        await Send.CreatedAtAsync($"/users/{result.Value}", new RegisterUserResponse(result.Value), cancellation: ct);

    }
}
