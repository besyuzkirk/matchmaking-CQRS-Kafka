using CQRS.Core.Infrastructure;
using Matchmaking.Cmd.Api.Commands;
using Matchmaking.Cmd.Api.DTOs;
using Matchmaking.Common.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Matchmaking.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class NewMatchController : ControllerBase
{
    private readonly ILogger<NewMatchController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public NewMatchController(ILogger<NewMatchController> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> NewMatchAsync(NewMatchCommand command)
    {
        var id = Guid.NewGuid();
        try
        {
            command.Id = id;

            await _commandDispatcher.SendAsync(command);

            return StatusCode(StatusCodes.Status201Created, new NewMatchResponse
            {
                Id = id,
                Message = "New match creation request completed successfully"
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Client made a bad request");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error!!!";
            _logger.Log(LogLevel.Error, ex,SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new NewMatchResponse
            {
                Id = id,
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}