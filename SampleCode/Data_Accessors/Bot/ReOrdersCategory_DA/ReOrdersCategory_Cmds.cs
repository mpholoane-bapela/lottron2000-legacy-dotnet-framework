using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersCategoryCommands : GenericDbCommandsExec<ReOrdersCategory>
{
    public ReOrdersCategoryCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { _dbPatternWrapper = dbPatternWrapper; }

}