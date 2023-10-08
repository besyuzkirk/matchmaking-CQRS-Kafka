using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Matchmaking.Cmd.Api.Commands;
using Matchmaking.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Matchmaking.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AddUserController : ControllerBase
{
    private readonly ILogger<AddUserController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;
    // GET
    public AddUserController(ILogger<AddUserController> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> AddUserToMatchAsync(AddUserToMatchCommand command)
    {
        var id = Guid.NewGuid();
        try
        {
            command.Id = id;

            await _commandDispatcher.SendAsync(command);

            return StatusCode(StatusCodes.Status201Created, new BaseResponse
            {
                Message = "User adding request completed successfully"
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect match ID targetting the aggregate!");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error while processing request to add a user to a match!";
            _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}