using Matchmaking.Query.Domain.Entities;
using Matchmaking.Query.Domain.Repositories;

namespace Matchmaking.Query.Api.Queries;

public class QueryHandler : IQueryHandler
{
    private readonly IMatchRepository _matchRepository;

    public QueryHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<MatchEntity>> HandleAsync(FindMatchesByUsername query)
    {
        return await _matchRepository.ListAllAsync(p =>
            p.UsernameOne == query.Username || p.UsernameTwo == query.Username || p.UsernameThree == query.Username);
    }
}