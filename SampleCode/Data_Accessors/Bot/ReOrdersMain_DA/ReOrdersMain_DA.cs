using Alexis.Infrastructure.DatabaseAccess;
using System.Threading.Tasks;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersMainDA : DataAccessorBase<ReOrdersMainQueries, ReOrdersMainCommands>
{
    public ReOrdersMainDA(ReOrdersMainQueries queries, ReOrdersMainCommands commands)
        : base(queries, commands)
    { }

    public ReOrdersMainDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { }

    public ReOrdersMainDA CreateNewInstance<C>() where C : DbContext_AlexisBase
    {
        var daPatternWrapper = Create_DAWrapper<C>();
        return new ReOrdersMainDA(daPatternWrapper);
    }

    // See if these can be moved to Alexis
    public void DisposeContext()
    { _dbPatternWrapper.Dispose(); }

    public async Task DisposeContextAsnyc()
    { await _dbPatternWrapper.DisposeAsync(); }

}