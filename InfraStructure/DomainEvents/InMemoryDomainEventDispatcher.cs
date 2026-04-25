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

    public async Task DispatchAsync(IDomainEvent @event, CancellationToken ct)
    {
        var eventType = @event.GetType(); //Gets Event Type eg: UserRegistered

        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType); // Creates IDomainEventHandler<eventType> eg: IDomainEventHandler<UserRegistered>

        var handlers = _serviceProvider.GetServices(handlerType); //Gets Handlers of type  IDomainEventHandler<eventType> 

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("HandleAsync"); //For Each Handler gets HandleAsync (Enforced by IDomainEventHandler)

            if(method != null)
                await (Task)method.Invoke(handler, new object[] { @event, ct })!; //Invokes HandleAsync recives Params Object and casts to Task (object => Task)
        }
    }
}
