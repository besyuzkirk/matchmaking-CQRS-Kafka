using CQRS.Core.Handlers;
using Matchmaking.Cmd.Domain.Aggregates;
using MongoDB.Driver;

namespace Matchmaking.Cmd.Api.Commands;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<MatchAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<MatchAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task HandleAsync(NewMatchCommand command)
    {
        var aggregate = new MatchAggregate(command.Id, command.UsernameOne, command.UsernameTwo,command.UsernameThree,command.MatchPoint);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(AddUserToMatchCommand command)
    {
        var aggregate = new AddUserToMatchAggregate(command.Id, command.Username, command.Point);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}