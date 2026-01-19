using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess
{
    public class RecommendationActionDA : DataAccessorBase<RecommendationActionQueries, RecommendationActionCommands>
    {
        public RecommendationActionDA(RecommendationActionQueries queries, RecommendationActionCommands commands)
            : base(queries, commands) { }

        public RecommendationActionDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper) { }


        // See if these can be moved to Alexis
        public void DisposeContext()
        { _dbPatternWrapper.Dispose(); }

        public async Task DisposeContextAsnyc()
        { await _dbPatternWrapper.DisposeAsync(); }

        public RecommendationActionDA CreateNewInstance<C>() where C : DbContext_AlexisBase
        {
            var daPatternWrapper = Create_DAWrapper<C>();
            return new RecommendationActionDA(daPatternWrapper);
        }
    }
}