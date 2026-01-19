using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceInstance_DbTableConfiguration : SchemaLogic_DbTableConfiguration_Base
    {

        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<BotInstance>();
            entity.ToTable("BotInstance", SchemaName);

            #region DB ID
            entity.Property(x => x.DbID).HasColumnName("DbID");
            entity.Property(x => x.DbID).HasColumnType("int");

            const int DbID_SEED = 1;
            const int DbID_INCREMENT = 1;

            modelBuilder.Entity<BotInstance>()
                 .Property(p => p.DbID)
                 .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

            modelBuilder.Entity<BotInstance>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            #endregion

            #region Column Lengths
            entity.Property(x => x.BotID).HasMaxLength(50);
            entity.Property(x => x.BotInstanceID).HasMaxLength(50);

            entity.Property(x => x.BudgetAllocationType).HasMaxLength(50);
            entity.Property(x => x.BudgetPercentageOfParent).HasColumnType("decimal(9,4)");
            entity.Property(x => x.Budget).HasColumnType("decimal(24,8)");

            entity.Property(x => x.DealOpen_Price_Initial).HasColumnType("decimal(24,8)");
            entity.Property(x => x.DealOpen_Price_Final).HasColumnType("decimal(24,8)");

            entity.Property(x => x.OrderVolume_Organic).HasColumnType("decimal(24,8)");
            entity.Property(x => x.OrderVolume_Calibrated).HasColumnType("decimal(24,8)");

            entity.Property(x => x.InstanceGap_Percent).HasColumnType("decimal(6,2)");
            entity.Property(x => x.DealOpen_FeeAmount).HasColumnType("decimal(24,8)");
            #endregion

            //----------------------------------------------------------------------------------

            #region VersionInfo

            modelBuilder.Entity<BotInstance>().OwnsOne(
            o => o.VersionInfo,
                sa =>
                {
                    sa.Property(p => p.Created).HasColumnName("Created");
                    sa.Property(p => p.PublishStatus).HasColumnName("PublishStatus");
                    sa.Property(x => x.PublishStatus).HasMaxLength(10);
                    sa.Property(x => x.Updated).HasColumnName("Updated");
                    sa.Property(x => x.UpdatedByUserID).HasColumnName("UpdatedByUserID");
                    sa.Property(x => x.UpdatedByUserID).HasMaxLength(50);

                    sa.Property(p => p.SystemCreated).HasColumnName("SystemCreated");
                    sa.Property(x => x.SystemUpdated).HasColumnName("SystemUpdated");
                }
                );
            #endregion

        }
    }
}