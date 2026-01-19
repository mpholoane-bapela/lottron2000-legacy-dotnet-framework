using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceCommands : GenericDbCommandsExec<BotInstance>
    {
        public BotInstanceCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
        { _dbPatternWrapper = dbPatternWrapper; }


    }

}
