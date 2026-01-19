using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class RecommendationFilterCommands : GenericDbCommandsExec<RecommendationFilter>
    {
        public RecommendationFilterCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
        { _dbPatternWrapper = dbPatternWrapper; }


    }
}