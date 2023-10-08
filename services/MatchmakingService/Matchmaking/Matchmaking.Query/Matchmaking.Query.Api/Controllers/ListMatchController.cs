using CQRS.Core.Infrastructure;
using Matchmaking.Common.DTOs;
using Matchmaking.Query.Api.DTOs;
using Matchmaking.Query.Api.Queries;
using Matchmaking.Query.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Matchmaking.Query.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ListMatchController : ControllerBase
{
    private readonly ILogger<ListMatchController> _logger;
    private readonly IQueryDispatcher<MatchEntity> _queryDispatcher;

    public ListMatchController(ILogger<ListMatchController> logger, IQueryDispatcher<MatchEntity> queryDispatcher)
    {
        _logger = logger;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetMatchesByUsernameAsync(string username)
    {   
        try
        {
            var matches = await _queryDispatcher.SendAsync(new FindMatchesByUsername
            {
                Username = username
            });
            return NormalResponse(matches);
        }
        catch (Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all posts!";
            return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
        }
    }
    
    private ActionResult NormalResponse(List<MatchEntity> matches)
    {
        if (matches == null || !matches.Any())
            return NoContent();

        var count = matches.Count;
        return Ok(new MatchResponse
        {
            Matches = matches,
            Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
        });
    }

    private ActionResult ErrorResponse(Exception ex, string safeErrorMessage)
    {
        _logger.LogError(ex, safeErrorMessage);

        return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
        {
            Message = safeErrorMessage
        });
    }
}
