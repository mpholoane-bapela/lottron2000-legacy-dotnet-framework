using Alexis.Infrastructure.DatabaseAccess;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotPartialTpDA : DataAccessorBase<BotPartialTPQueries, BotPartialTPCommands>
    {
        public BotPartialTpDA(BotPartialTPQueries queries, BotPartialTPCommands commands)
            : base(queries, commands) { }

        public BotPartialTpDA(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper) { }


        // See if these can be moved to Alexis
        public void DisposeContext()
        { _dbPatternWrapper.Dispose(); }

        public async Task DisposeContextAsnyc()
        { await _dbPatternWrapper.DisposeAsync(); }

        public BotPartialTpDA CreateNewInstance<C>() where C : DbContext_AlexisBase
        {
            var daPatternWrapper = Create_DAWrapper<C>();
            return new BotPartialTpDA(daPatternWrapper);
        }
    }
}