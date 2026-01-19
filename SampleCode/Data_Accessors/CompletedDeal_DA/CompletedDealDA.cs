using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess;

public class CompletedDealDA : DataAccessorBase<CompletedDealQueries, CompletedDealCommands>
{
    public CompletedDealDA(CompletedDealQueries queries, CompletedDealCommands commands)
        : base(queries, commands)
    { }

    public CompletedDealDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { }

    // See if these can be moved to Alexis
    public void DisposeContext()
    { _dbPatternWrapper.Dispose(); }

    public async Task DisposeContextAsnyc()
    { await _dbPatternWrapper.DisposeAsync(); }

    public CompletedDealDA CreateNewInstance<C>() where C : DbContext_AlexisBase
    {
        var daPatternWrapper = Create_DAWrapper<C>();
        return new CompletedDealDA(daPatternWrapper);
    }
}