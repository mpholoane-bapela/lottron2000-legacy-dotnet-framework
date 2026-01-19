using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotCommands : GenericDbCommandsExec<Bot>
    {
        public BotCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
        { _dbPatternWrapper = dbPatternWrapper; }


    }
}