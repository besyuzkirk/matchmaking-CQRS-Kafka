using System.Linq.Expressions;
using Matchmaking.Query.Domain.Entities;

namespace Matchmaking.Query.Domain.Repositories;

public interface IMatchRepository
{
    Task CreateAsync(MatchEntity match);
    Task UpdateAsync(MatchEntity match);
    Task DeleteAsync(Guid matchId);
    Task<MatchEntity> GetByIdAsync(Guid matchId);
    Task<List<MatchEntity>> ListAllAsync(Expression<Func<MatchEntity, bool>> predicate, bool noTracking = false);
}