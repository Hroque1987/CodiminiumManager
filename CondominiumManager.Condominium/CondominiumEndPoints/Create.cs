using CondominiumManager.Condominium.Application.Contracts.Commands;
using CondominiumManager.Condominium.Application.Usecases;
using CondominiumManager.Condominium.CondominiumEndPoints.Requests;
using CondominiumManager.Identity.UserEndPoints.Mapping;
using CondominiumManager.Identity.UserEndPoints.Responses;
using FastEndpoints;
using InfraStructure.FastEndPoints.PostProcessors;
using InfraStructure.FastEndPoints.PreProcessors;
using Microsoft.AspNetCore.Http;

namespace CondominiumManager.Condominium.CondominiumEndPoints;

internal class Create : Endpoint<CreateBuildingRequest, CreateBuildingResponse>
{
    private readonly CreateBuildingHandler _handler;

    public Create(CreateBuildingHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Post("/buildings");
        PreProcessor<LoggingPreProcessor<CreateBuildingRequest>>();
        PostProcessor<LoggingPostProcessor<CreateBuildingRequest, CreateBuildingResponse>>();
        Policies("Building");
        
    }

    public override async Task HandleAsync(CreateBuildingRequest req, CancellationToken ct)
    {
        var command = new CreateBuildingCommand(
                   req.Name,
                   req.Street,
                   req.DoorNumber,
                   req.PostalCode,
                   req.City,
                   req.Country);

        var result = await _handler.HandleAsync(command, ct);


        if (result.ToHttpError() is IResult error)
        {
            await Send.ResultAsync(error);
            return;
        }

        await Send.CreatedAtAsync($"/buildigs/{result.Value}", new CreateBuildingResponse(result.Value), cancellation: ct);

       
    }
}