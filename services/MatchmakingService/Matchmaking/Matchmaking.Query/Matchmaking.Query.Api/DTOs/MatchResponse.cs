using Matchmaking.Common.DTOs;
using Matchmaking.Query.Domain.Entities;

namespace Matchmaking.Query.Api.DTOs;

public class MatchResponse : BaseResponse
{
    public List<MatchEntity> Matches { get; set; }
}