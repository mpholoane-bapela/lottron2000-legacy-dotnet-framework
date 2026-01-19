using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess;

public class RecommendationFilterDA : DataAccessorBase<RecommendationFilterQueries, RecommendationFilterCommands>
{
    public RecommendationFilterDA(RecommendationFilterQueries queries, RecommendationFilterCommands commands)
        : base(queries, commands)
    { }

    public RecommendationFilterDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { }


    // See if these can be moved to Alexis
    public void DisposeContext()
    { _dbPatternWrapper.Dispose(); }

    public async Task DisposeContextAsnyc()
    { await _dbPatternWrapper.DisposeAsync(); }

    public RecommendationFilterDA CreateNewInstance<C>() where C : DbContext_AlexisBase
    {
        var daPatternWrapper = Create_DAWrapper<C>();
        return new RecommendationFilterDA(daPatternWrapper);
    }

}