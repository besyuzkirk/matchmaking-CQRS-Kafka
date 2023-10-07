using Matchmaking.Common.DTOs;

namespace Matchmaking.Cmd.Api.DTOs;

public class NewMatchResponse : BaseResponse
{
    public Guid Id { get; set; }
}