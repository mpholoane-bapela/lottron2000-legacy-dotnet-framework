using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceDA : DataAccessorBase<BotInstanceQueries, BotInstanceCommands>
    {
        public BotInstanceDA(BotInstanceQueries queries, BotInstanceCommands commands) : base(queries, commands) { }

        public BotInstanceDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper) { }

        // See if these can be moved to Alexis
        public void DisposeContext()
        { _dbPatternWrapper.Dispose(); }

        public async Task DisposeContextAsnyc()
        { await _dbPatternWrapper.DisposeAsync(); }

        public BotInstanceDA CreateNewInstance<C>() where C : DbContext_AlexisBase
        {
            var daPatternWrapper = Create_DAWrapper<C>();
            return new BotInstanceDA(daPatternWrapper);
        }

    }
}