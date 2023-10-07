using Matchmaking.Query.Domain.Entities;
using Matchmaking.Query.Domain.Repositories;
using Matchmaking.Query.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Matchmaking.Query.Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public MatchRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(MatchEntity match)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Matches.Add(match);

            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid matchId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var match = await GetByIdAsync(matchId);

            if (match == null) return;

            context.Matches.Remove(match);
            _ = await context.SaveChangesAsync();
        }

        public async Task<MatchEntity> GetByIdAsync(Guid matchId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Matches
                .FirstOrDefaultAsync(x => x.MatchId == matchId);
        }

        public async Task<List<MatchEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Matches.AsNoTracking()
                   .ToListAsync();
        }


        public async Task UpdateAsync(MatchEntity match)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Matches.Update(match);

            _ = await context.SaveChangesAsync();
        }
    }