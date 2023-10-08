using CQRS.Core.Queries;

namespace Matchmaking.Query.Api.Queries;

public class FindMatchesByUsername : BaseQuery
{
    public string Username { get; set; }
}