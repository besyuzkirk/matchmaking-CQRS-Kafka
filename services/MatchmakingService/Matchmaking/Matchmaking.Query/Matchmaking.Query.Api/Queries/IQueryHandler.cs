using Matchmaking.Query.Domain.Entities;

namespace Matchmaking.Query.Api.Queries;

public interface IQueryHandler
{
    Task<List<MatchEntity>> HandleAsync(FindMatchesByUsername query);
}