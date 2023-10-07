using Matchmaking.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Matchmaking.Query.Infrastructure.DataAccess;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<MatchEntity> Matches { get; set; }
}