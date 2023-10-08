using CQRS.Core.Events;

namespace Matchmaking.Common.Events;

public class AddUserToMatchEvent : BaseEvent
{
    public AddUserToMatchEvent() : base(nameof(AddUserToMatchEvent))
    {
    }

    public string Username { get; set; }
    public int Point { get; set; }
}