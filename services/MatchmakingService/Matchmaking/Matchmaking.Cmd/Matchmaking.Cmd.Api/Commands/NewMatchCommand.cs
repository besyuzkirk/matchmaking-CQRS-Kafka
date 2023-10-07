using CQRS.Core.Commands;

namespace Matchmaking.Cmd.Api.Commands;

public class NewMatchCommand : BaseCommand
{
    public string UsernameOne { get; set; }
    public string UsernameTwo { get; set; }
    public string UsernameThree { get; set; }
    public string Status { get; set; }
    public int Point { get; set; }
}