using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersMainCommands : GenericDbCommandsExec<ReOrdersMain>
{
    public ReOrdersMainCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { _dbPatternWrapper = dbPatternWrapper; }

}