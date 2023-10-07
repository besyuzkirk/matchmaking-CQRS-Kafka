using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Matchmaking.Cmd.Domain.Aggregates;

namespace Matchmaking.Cmd.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<MatchAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<MatchAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new MatchAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }
}