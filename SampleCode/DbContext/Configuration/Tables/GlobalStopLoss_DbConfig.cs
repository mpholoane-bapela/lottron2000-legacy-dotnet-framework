using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class GlobalStopLoss_DbConfig : SchemaLogic_DbTableConfiguration_Base
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<GlobalStopLoss>();
            entity.ToTable("GlobalStopLoss", SchemaName);

            #region DB ID
            modelBuilder.Entity<GlobalStopLoss>().HasKey(p => p.GlobalStopLossID);

            entity.Property(x => x.DbID).HasColumnName("DbID");
            entity.Property(x => x.DbID).HasColumnType("int");

            const int DbID_SEED = 1;
            const int DbID_INCREMENT = 1;

            modelBuilder.Entity<GlobalStopLoss>()
                 .Property(p => p.DbID)
                 .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

            modelBuilder.Entity<GlobalStopLoss>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            #endregion

            #region Column Lengths
            entity.Property(x => x.BotID).HasMaxLength(50);
            entity.Property(x => x.GlobalStopLossID).HasMaxLength(50);
            entity.Property(x => x.ActivationPoint_OffSetPercent).HasColumnType("decimal(8,4)");
            entity.Property(x => x.Activation_CalculatedPrice).HasColumnType("decimal(24,8)");
            entity.Property(x => x.TakeLossPoint_OffSetPercent).HasColumnType("decimal(8,4)");
            entity.Property(x => x.TakeLoss_CalculatedPrice).HasColumnType("decimal(24,8)");
            entity.Property(x => x.TakeLoss_FinalPrice).HasColumnType("decimal(24,8)");

            entity.Property(x => x.Volume_Calculated).HasColumnType("decimal(24,8)");
            entity.Property(x => x.PositionAmount_Percent).HasColumnType("decimal(8,4)");
            entity.Property(x => x.PositionAmount_Reserved).HasColumnType("decimal(24,8)");

            entity.Property(x => x.DealID).HasMaxLength(50);
            entity.Property(x => x.ApiOrderID).HasMaxLength(50);
            entity.Property(x => x.ApiTimeStamp_Current).HasMaxLength(50);
            entity.Property(x => x.ApiTimeStamp_DealOpen).HasMaxLength(50);
            entity.Property(x => x.ApiTimeStamp_DealClose).HasMaxLength(50);
            #endregion

            //----------------------------------------------------------------------------------

            #region VersionInfo

            modelBuilder.Entity<GlobalStopLoss>().OwnsOne(
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