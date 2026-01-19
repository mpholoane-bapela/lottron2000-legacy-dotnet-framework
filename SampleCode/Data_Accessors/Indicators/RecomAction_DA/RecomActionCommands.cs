using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class RecommendationActionCommands : GenericDbCommandsExec<RecommendationAction>
    {
        public RecommendationActionCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
        { _dbPatternWrapper = dbPatternWrapper; }

    }
}