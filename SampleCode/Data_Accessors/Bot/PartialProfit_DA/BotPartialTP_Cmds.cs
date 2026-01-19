using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class BotPartialTPCommands : GenericDbCommandsExec<BotPartialTP>
{
    public BotPartialTPCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    { _dbPatternWrapper = dbPatternWrapper; }

}