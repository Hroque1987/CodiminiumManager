using Microsoft.Extensions.DependencyInjection;
using Sharedkernel.DomainEvents;

namespace InfraStructure.DomainEvents;

public class InMemoryDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryDomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(TEvent @event, CancellationToken ct) where TEvent : IDomainEvent
    {

        var handlers = _serviceProvider.GetServices<IDomainEventHandler<TEvent>>();//Gets Event Handler Service


        foreach (var handler in handlers)
        {
     
            await handler.HandleAsync(@event, ct); 
            
        }
    }
}
