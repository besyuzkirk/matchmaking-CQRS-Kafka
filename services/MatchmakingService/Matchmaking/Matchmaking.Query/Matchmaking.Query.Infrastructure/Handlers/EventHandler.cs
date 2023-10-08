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
            UsernameThree = @event.UsernameThree,
            MatchPoint = @event.MatchPoint
        };

        await _matchRepository.CreateAsync(match);
    }

    public async Task On(AddUserToMatchEvent @event)
    {
        var firstMatch = new MatchEntity();
        var matches = await _matchRepository.ListAllAsync(
            p => p.Status < 3
                 && p.MatchPoint > (@event.Point - 10)
                 && p.MatchPoint < (@event.Point + 10),
            true);
        
        if(matches != null)
            firstMatch = matches.MinBy(m => m.MatchCreatedAt);
        
        if (firstMatch == null)
        {
            await _matchRepository.CreateAsync(new MatchEntity
            {
                MatchCreatedAt = DateTime.UtcNow,
                Status = 1,
                UsernameOne = @event.Username,
                MatchPoint = @event.Point
            });
        }
        else
        {
            await _matchRepository.UpdateAsync(new MatchEntity
            {
                MatchId = firstMatch.MatchId,
                MatchPoint = firstMatch.Status == 1 ? (@event.Point + firstMatch.MatchPoint) / 2 : (@event.Point + 2 * firstMatch.MatchPoint) / 3,
                UsernameOne = firstMatch.UsernameOne,
                UsernameTwo = firstMatch.Status == 1 ? @event.Username : firstMatch.UsernameTwo,
                UsernameThree = firstMatch.Status == 2 ? @event.Username : firstMatch.UsernameTwo,
                Status = firstMatch.Status + 1
            });
        }
    }
}