using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess;

public class GlobalStopLossDA : DataAccessorBase<GlobalStopLossQueries, GlobalStopLossCommands>
{
    public GlobalStopLossDA(GlobalStopLossQueries queries, GlobalStopLossCommands commands)
        : base(queries, commands)
    { }

    public GlobalStopLossDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { }


    // See if these can be moved to Alexis
    public void DisposeContext()
    { _dbPatternWrapper.Dispose(); }

    public async Task DisposeContextAsnyc()
    { await _dbPatternWrapper.DisposeAsync(); }


    public GlobalStopLossDA CreateNewInstance<C>() where C : DbContext_AlexisBase
    {
        var daPatternWrapper = Create_DAWrapper<C>();
        return new GlobalStopLossDA(daPatternWrapper);
    }
}