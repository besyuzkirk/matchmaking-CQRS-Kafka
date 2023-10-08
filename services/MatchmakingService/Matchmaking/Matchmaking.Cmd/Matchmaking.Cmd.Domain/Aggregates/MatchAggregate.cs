using CQRS.Core.Domain;
using Matchmaking.Common.Events;

namespace Matchmaking.Cmd.Domain.Aggregates;

public class MatchAggregate : AggregateRoot
{
    private bool _active;
    private string _usernameOne;
    private int _matchPoint;
    private int _status;
   
    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public MatchAggregate()
    {
        
    }
    
    
    public MatchAggregate(Guid id, string usernameOne, string usernameTwo, string usernameThree ,int matchPoint)
    {
        RaiseEvent(new MatchCreatedEvent
        {
            Id = id,
            Status = 3,
            MatchCreatedDate = DateTime.Now,
            MatchPoint = matchPoint,
            UsernameOne = usernameOne,
            UsernameTwo = usernameTwo,
            UsernameThree = usernameThree
        });
    }

    public void Apply(MatchCreatedEvent @event)
    {
        _id = @event.Id;
        _active = true;
        _status = 1;
        _matchPoint = @event.MatchPoint;
        _usernameOne = @event.UsernameOne;
    }
}