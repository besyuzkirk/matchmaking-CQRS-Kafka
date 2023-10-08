using CQRS.Core.Commands;

namespace Matchmaking.Cmd.Api.Commands;

public class AddUserToMatchCommand : BaseCommand
{
    public string Username { get; set; }
    public int Point { get; set; }
}