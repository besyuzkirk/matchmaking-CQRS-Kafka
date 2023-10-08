using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Matchmaking.Query.Domain.Entities;

namespace Matchmaking.Query.Infrastructure.Dispatchers;

public class QueryDispatcher : IQueryDispatcher<MatchEntity>
{
    private readonly Dictionary<Type, Func<BaseQuery, Task<List<MatchEntity>>>> _handlers = new();

    public void RegisterHandler<TQuery>(Func<TQuery, Task<List<MatchEntity>>> handler) where TQuery : BaseQuery
    {
        if (_handlers.ContainsKey(typeof(TQuery)))
        {
            throw new IndexOutOfRangeException("You cannot register the same query handler twice!");
        }

        _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
    }

    public async Task<List<MatchEntity>> SendAsync(BaseQuery query)
    {
        if (_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<MatchEntity>>> handler))
        {
            return await handler(query);
        }

        throw new ArgumentNullException(nameof(handler), "No query handler was registered!");
    }
}