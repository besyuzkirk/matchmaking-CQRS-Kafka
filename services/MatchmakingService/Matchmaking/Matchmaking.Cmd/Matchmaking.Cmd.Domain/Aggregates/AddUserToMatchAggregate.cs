using CQRS.Core.Domain;
using Matchmaking.Common.Events;

namespace Matchmaking.Cmd.Domain.Aggregates;

public class AddUserToMatchAggregate : AggregateRoot
{
    private bool _active;
    private string _usernameOne;
    private int _point;
    
    public bool Active
    {
        get => _active;
        set => _active = value;
    }
    
    public AddUserToMatchAggregate()
    {
        
    }
    
    
    public AddUserToMatchAggregate(Guid id, string username, int point)
    {
        RaiseEvent(new AddUserToMatchEvent
        {
            Id = id,
            Username = username,
            Point = point
        });
    }
    
    public void Apply(AddUserToMatchEvent @event)
    {
        _id = @event.Id;
        _active = true;     
        _point = @event.Point;
        _usernameOne = @event.Username;
    }
}