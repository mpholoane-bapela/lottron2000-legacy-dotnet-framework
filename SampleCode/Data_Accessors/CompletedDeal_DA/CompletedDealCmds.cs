using Alexis.Infrastructure.DatabaseAccess;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class CompletedDealCommands : GenericDbCommandsExec<CompletedDeal>
{
    public CompletedDealCommands(DbAccessPatternWrapper dbPatternWrapper) : base(dbPatternWrapper)
    {
        _dbPatternWrapper = dbPatternWrapper;
    }

    // NO need to override commands
}