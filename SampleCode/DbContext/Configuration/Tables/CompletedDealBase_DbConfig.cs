using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TradingBots.CrossCutters.Models;

namespace TradingBots.Native.Infra.DbAccess
{
    public static class CompletedDeal_DbConfig_Base
    {
        public static void ConfigureModel(ModelBuilder modelBuilder, string schemaName)
        {
            var entity = modelBuilder.Entity<CompletedDealBase>();
            entity.ToTable("CompletedDeal", schemaName);

            modelBuilder.Entity<CompletedDealBase>()
            .HasDiscriminator<int>("__CompletedDeal_Type")
            .HasValue<CompletedDealBase>(0);


            #region DB ID
            modelBuilder.Entity<CompletedDealBase>().HasKey(p => p.CompletedDealID);

            entity.Property(x => x.DbID).HasColumnName("DbID");
            entity.Property(x => x.DbID).HasColumnType("int");

            const int DbID_SEED = 1;
            const int DbID_INCREMENT = 1;

            modelBuilder.Entity<CompletedDealBase>()
                 .Property(p => p.DbID)
                 .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

            modelBuilder.Entity<CompletedDealBase>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            #endregion


            #region Column Lengths
            entity.Property(x => x.CompletedDealID).HasMaxLength(50);
            entity.Property(x => x.BotID).HasMaxLength(50);
            entity.Property(x => x.BotInstanceID).HasMaxLength(100);
            entity.Property(x => x.Amount_CurrencySymbol).HasMaxLength(20);
            entity.Property(x => x.Amount_Total_Calibrated).HasColumnType("decimal(24,8)");

            entity.Property(x => x.Amount_Total_Organic).HasColumnType("decimal(24,8)");
            entity.Property(x => x.ProfitAmount_DCA).HasColumnType("decimal(18,6)");
            entity.Property(x => x.ProfitAmount_SingleDeal).HasColumnType("decimal(18,6)");
            entity.Property(x => x.DealID).HasMaxLength(100);
            entity.Property(x => x.BotSetUp_TradingPair).HasMaxLength(50);


            entity.Property(x => x.ApiOrderID_DealOpen).HasMaxLength(50);
            entity.Property(x => x.ApiOrderID_DealClose).HasMaxLength(50);

            entity.Property(x => x.Volume_Calibrated).HasColumnType("decimal(24,8)");
            entity.Property(x => x.Volume_Organic).HasColumnType("decimal(24,8)");
            entity.Property(x => x.DealOpen_Price).HasColumnType("decimal(24,8)");
            entity.Property(x => x.DealClose_Price).HasColumnType("decimal(24,8)");
            entity.Property(x => x.TimeToComplete_Seconds).HasColumnType("decimal(10,0)");

            entity.Property(x => x.TradingFees_To_ProfitAmount_DCA).HasColumnType("decimal(14,6)");
            entity.Property(x => x.TradingFees_To_ProfitAmount_SingleDeal).HasColumnType("decimal(14,6)");
            entity.Property(x => x.ROC_Bot_SingleDeal_Fraction).HasColumnType("decimal(14,6)");
            entity.Property(x => x.ROC_Instance_SingleDeal_Fraction).HasColumnType("decimal(14,6)");
            entity.Property(x => x.ProfitAmount_FractionOfTotal_DCA).HasColumnType("decimal(18,6)");
            entity.Property(x => x.ProfitAmount_FractionOfTotal_SingleDeal).HasColumnType("decimal(18,6)");
            entity.Property(x => x.ROC_Account_SingleDeal_Fraction).HasColumnType("decimal(18,6)");


            entity.Property(x => x.CalibrationFactor).HasColumnType("decimal(5,2)");
            entity.Property(x => x.ChangeFraction_Organic).HasColumnType("decimal(12,4)");
            entity.Property(x => x.ChangeFraction_Calibrated).HasColumnType("decimal(12,4)");

            entity.Property(x => x.ROC_Instance_DCA_Fraction).HasColumnType("decimal(14,6)");
            entity.Property(x => x.ROC_Bot_DCA_Fraction).HasColumnType("decimal(14,6)");
            entity.Property(x => x.ROC_Account_DCA_Fraction).HasColumnType("decimal(14,6)");

            entity.Property(x => x.BotSetUp_InstanceGap_Trading).HasColumnType("decimal(24,8)");
            entity.Property(x => x.BotSetUp_InstanceGap_Absolute).HasColumnType("decimal(24,8)");
            entity.Property(x => x.BotSetUp_TPCondition).HasColumnType("decimal(24,8)");
            entity.Property(x => x.Instance_TTP_EventualTrade_Price).HasColumnType("decimal(24,8)");
            entity.Property(x => x.Instance_TTP_Extra_Fraction).HasColumnType("decimal(14,6)");
            entity.Property(x => x.TradingFees_Total).HasColumnType("decimal(24,8)");

            entity.Property(x => x.BotSetUp_StopLoss_TriggerChangeAmount).HasColumnType("decimal(24,8)");
            entity.Property(x => x.BotSetUp_StopOrder_Trigger_Price).HasColumnType("decimal(24,8)");
            entity.Property(x => x.BotSetUp_StopLoss_EventualTradePrice).HasColumnType("decimal(24,8)");

            #endregion

            //----------------------------------------------------------------------------------

            entity.Property(x => x.TimeToComplete_TimeSpan).HasConversion<long>();

            //----------------------------------------------------------------------------------

            #region VersionInfo

            modelBuilder.Entity<CompletedDealBase>().OwnsOne(
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