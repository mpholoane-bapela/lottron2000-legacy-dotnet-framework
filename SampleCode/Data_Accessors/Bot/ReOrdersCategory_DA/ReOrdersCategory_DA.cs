using Alexis.Infrastructure.DatabaseAccess;
using System.Threading.Tasks;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersCategoryDA : DataAccessorBase<ReOrdersCategoryQueries, ReOrdersCategoryCommands>
{
    public ReOrdersCategoryDA(ReOrdersCategoryQueries queries, ReOrdersCategoryCommands commands)
        : base(queries, commands)
    { }

    public ReOrdersCategoryDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { }


    public ReOrdersCategoryDA CreateNewInstance<C>() where C : DbContext_AlexisBase
    {
        var daPatternWrapper = Create_DAWrapper<C>();
        return new ReOrdersCategoryDA(daPatternWrapper);
    }

    // See if these can be moved to Alexis
    public void DisposeContext()
    { _dbPatternWrapper.Dispose(); }

    public async Task DisposeContextAsnyc()
    { await _dbPatternWrapper.DisposeAsync(); }

}