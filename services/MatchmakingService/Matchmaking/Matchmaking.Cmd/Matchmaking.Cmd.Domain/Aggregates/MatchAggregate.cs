using CQRS.Core.Domain;
using Matchmaking.Common.Events;

namespace Matchmaking.Cmd.Domain.Aggregates;

public class MatchAggregate : AggregateRoot
{
    private bool _active;
    private string _status;
    
    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public MatchAggregate()
    {
        
    }
    
    
    public MatchAggregate(Guid id, string status)
    {
        RaiseEvent(new MatchCreatedEvent
        {
            Id = id,
            Status = status,
            MatchCreatedDate = DateTime.Now,
        });
    }

    public void Apply(MatchCreatedEvent @event)
    {
        _id = @event.Id;
        _active = true;
        _status = @event.Status;
    }

    public void AddUserToMatch(string username)
    {
        if (!_active)
        {
            throw new InvalidOperationException("You cannot add the user of an inactive match!");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new InvalidOperationException($"The value of {nameof(username)} cannot be null or empty. Please provide a valid {nameof(username)}!");
        }

        RaiseEvent(new AddUserToMatchEvent
        {
            Id = _id,
            Username = username
        });
    }
    
    public void Apply(AddUserToMatchEvent @event)
    {
        _id = @event.Id;
        _active = true;
    }
}