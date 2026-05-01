using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sharedkernel.Abstractions;
using Sharedkernel.DomainEvents;

namespace CondominiumManager.Condominium.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CondominiumDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(CondominiumDbContext context, IServiceProvider serviceProvider)
    {       
        _context = context;
        _serviceProvider = serviceProvider;
    }



    public async Task CommitAsync(CancellationToken ct = default)
    {
      



        var entities = _context.ChangeTracker.Entries<EventStorage>()
                       .Select(e => e.Entity)
                       .Where(e => e.DomainEvents != null && e.DomainEvents.Count != 0)
                       .ToList();

        var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();


        entities.ForEach(e => e.ClearDomainEvents());

        foreach (var @event in domainEvents)
        {
            await Dispatch(@event, ct);
        }

        await _context.SaveChangesAsync(ct);

    }

    private async Task Dispatch(IDomainEvent @event, CancellationToken ct)
    {
        var dispatcherType = typeof(IDomainEventHandler<>)
            .MakeGenericType(@event.GetType());

        var handlers = _serviceProvider.GetServices(dispatcherType);

        foreach (var handler in handlers)
        {
            var method = dispatcherType.GetMethod("HandleAsync")!;
            await (Task)method.Invoke(handler, new object[] { @event, ct })!;
        }
    }
}
