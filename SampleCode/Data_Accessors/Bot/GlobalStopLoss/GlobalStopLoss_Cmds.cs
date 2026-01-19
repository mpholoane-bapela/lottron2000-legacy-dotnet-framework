using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class GlobalStopLossCommands : GenericDbCommandsExec<GlobalStopLoss>
{
    public GlobalStopLossCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { _dbPatternWrapper = dbPatternWrapper; }

}