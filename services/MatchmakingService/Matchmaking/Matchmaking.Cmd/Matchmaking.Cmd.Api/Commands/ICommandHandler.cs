namespace Matchmaking.Cmd.Api.Commands;

public interface ICommandHandler
{
    Task HandleAsync(NewMatchCommand command);
    Task HandleAsync(AddUserToMatchCommand command);
}