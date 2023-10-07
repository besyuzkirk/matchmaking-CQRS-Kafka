using CQRS.Core;
using Matchmaking.Common.Events;
using Matchmaking.Query.Domain.Entities;
using Matchmaking.Query.Domain.Repositories;

namespace Matchmaking.Query.Infrastructure.Handlers;

public class EventHandler : IEventHandler
{
    private readonly IMatchRepository _matchRepository;

    public EventHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }
    
    public async Task On(MatchCreatedEvent @event)
    {
        var match = new MatchEntity
        {
            MatchId = @event.Id,
            Status = @event.Status,
            UsernameOne = @event.UsernameOne,
            UsernameTwo = @event.UsernameTwo,
            UsernameThree = @event.UsernameThree
        };

        await _matchRepository.CreateAsync(match);
    }

    public async Task On(AddUserToMatchEvent @event)
    {
        var match = await _matchRepository.GetByIdAsync(@event.Id);
        
        if(match == null) return;

        match.UsernameOne = @event.Username;
        await _matchRepository.UpdateAsync(match);
    }
}