using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceStateDA : DataAccessorBase<BotInstanceStateQueries, BotInstanceStateCommands>
    {
        public BotInstanceStateDA(BotInstanceStateQueries queries, BotInstanceStateCommands commands) : base(queries, commands)
        { }

        public BotInstanceStateDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper) { }


        // See if these can be moved to Alexis
        public void DisposeContext()
        { _dbPatternWrapper.Dispose(); }

        public async Task DisposeContextAsnyc()
        { await _dbPatternWrapper.DisposeAsync(); }

        public BotInstanceStateDA CreateNewInstance<C>() where C : DbContext_AlexisBase
        {
            var daPatternWrapper = Create_DAWrapper<C>();
            return new BotInstanceStateDA(daPatternWrapper);
        }
    }

}