using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotDA : DataAccessorBase<BotQueries, BotCommands>
    {
        public BotDA(BotQueries queries, BotCommands commands) : base(queries, commands) { }

        public BotDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper) { }

        // See if these can be moved to Alexis
        public void DisposeContext()
        { _dbPatternWrapper.Dispose(); }

        public async Task DisposeContextAsnyc()
        { await _dbPatternWrapper.DisposeAsync(); }

        public BotDA CreateNewInstance<C>() where C : DbContext_AlexisBase
        {
            var daPatternWrapper = Create_DAWrapper<C>();
            return new BotDA(daPatternWrapper);
        }
    }
}