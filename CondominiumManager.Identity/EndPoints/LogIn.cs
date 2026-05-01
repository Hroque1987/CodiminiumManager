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

internal class LogIn(LoginUserHandler loginUserHandler) : Endpoint<LogInRequest, LogInResponse>
{
    private readonly LoginUserHandler _loginUserHandler = loginUserHandler;

    public override void Configure()
    {
        Post("/login");
        PreProcessor<LoggingPreProcessor<LogInRequest>>();
        PostProcessor<LoggingPostProcessor<LogInRequest, LogInResponse>>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(LogInRequest req, CancellationToken ct)
    {
        var command = new LoginUserCommand(req.Email, req.Password);

        var result = await _loginUserHandler.HandleAsync(command, ct);


        if (result.ToHttpError() is IResult error)
        {
            await Send.ResultAsync(error);
            return;
        }

        await Send.OkAsync(new LogInResponse(result.Value), cancellation: ct);
    }
}
