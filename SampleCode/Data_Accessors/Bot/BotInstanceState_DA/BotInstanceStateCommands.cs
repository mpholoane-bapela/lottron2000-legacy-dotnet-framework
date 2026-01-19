using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceStateCommands : GenericDbCommandsExec<BotInstanceState>
    {
        public BotInstanceStateCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
        { _dbPatternWrapper = dbPatternWrapper; }

    }

}