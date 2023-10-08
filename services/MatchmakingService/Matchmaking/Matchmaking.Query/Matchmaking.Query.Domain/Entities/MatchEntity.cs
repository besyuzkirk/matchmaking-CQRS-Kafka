using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matchmaking.Query.Domain.Entities;

[Table("Match")]
public class MatchEntity
{
    [Key] 
    public Guid MatchId { get; set; }
    public string? UsernameOne { get; set; }
    public string? UsernameTwo { get; set; }
    public string? UsernameThree { get; set; }
    public int MatchPoint { get; set; }
    public int Status { get; set; }
    public DateTime MatchCreatedAt { get; set; }
}