using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class CompletedDeal_DbConfig : SchemaLogic_DbTableConfiguration_Base
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            CompletedDeal_DbConfig_Base.ConfigureModel(modelBuilder, SchemaName);

            modelBuilder.Entity<CompletedDeal>()
            .HasDiscriminator<int>("__CompletedDeal_Type")
            .HasValue<CompletedDeal>(1);

        }
    }
}