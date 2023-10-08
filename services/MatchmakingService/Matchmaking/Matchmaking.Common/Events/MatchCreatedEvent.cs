using CQRS.Core.Events;

namespace Matchmaking.Common.Events;

public class MatchCreatedEvent : BaseEvent
{
    public MatchCreatedEvent() : base(nameof(MatchCreatedEvent))
    {
    }
    
    public string UsernameOne { get; set; }
    public string UsernameTwo { get; set; }
    public string UsernameThree { get; set; }
    public int Status { get; set; }
    public int MatchPoint { get; set; }
    public DateTime MatchCreatedDate { get; set; }
}