using Matchmaking.Common.Events;

namespace Matchmaking.Query.Infrastructure.Handlers;

public interface IEventHandler
{
    Task On(MatchCreatedEvent @event);
    Task On(AddUserToMatchEvent @event);
}