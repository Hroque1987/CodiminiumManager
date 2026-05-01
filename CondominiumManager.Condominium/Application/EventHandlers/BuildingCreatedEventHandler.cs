using CondominiumManager.Condominium.Application.Abstractions;
using CondominiumManager.Condominium.Domain.Entities;
using CondominiumManager.Condominium.Domain.Enums;
using CondominiumManager.Condominium.Domain.Events;
using Sharedkernel.Abstractions;
using Sharedkernel.DomainEvents;

namespace CondominiumManager.Condominium.Application.EventHandlers;

internal class BuildingCreatedEventHandler : IDomainEventHandler<BuildingCreatedEvent>
{
    private readonly IMembershipRepository _repository;
    private readonly ICurrentUser _currentUser;

    public BuildingCreatedEventHandler(IMembershipRepository repository, ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }
    public Task HandleAsync(BuildingCreatedEvent @event, CancellationToken ct = default)
    {
        var membership = Membership.Create(_currentUser.Id, @event.BuildingId, Role.Admin);
       
        if (membership.IsSuccess)
        {
            _repository.Add(membership.Value);
        }
        return Task.CompletedTask;

    }
}
