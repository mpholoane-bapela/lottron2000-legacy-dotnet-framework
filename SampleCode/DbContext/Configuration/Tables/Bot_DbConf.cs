using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class Bot_DbTableConfiguration : SchemaLogic_DbTableConfiguration_Base
    {
        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Bot>();
            entity.ToTable("Bot_01_Main", SchemaName).HasOne<Bot>().WithOne().HasForeignKey<Bot>(a => a.BotID);


            #region DB ID
            entity.Property(x => x.BotID).HasMaxLength(50);

            modelBuilder.Entity<Bot>().HasKey(p => p.BotID);


            entity.Property(x => x.DbID).HasColumnName("DbID");
            entity.Property(x => x.DbID).HasColumnType("int");

            const int DbID_SEED = 1;
            const int DbID_INCREMENT = 1;

            modelBuilder.Entity<Bot>()
                 .Property(p => p.DbID)
                 .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

            modelBuilder.Entity<Bot>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            #endregion


            SetProperties(entity);
            SplitSectionsToTables(entity);

        }

        private void SetProperties(EntityTypeBuilder<Bot> entity)
        {
            #region MAIN

            entity.Property(x => x.Title).HasMaxLength(100);
            entity.Property(x => x.TradingPair).HasMaxLength(50);
            entity.Property(x => x.Budget_TotalForBot).HasColumnType("decimal(24,8)").HasColumnName("Budget");
            entity.Property(x => x.DealOpen_PriceLimit_Default).HasColumnType("decimal(24,8)").HasColumnName("PriceLimit_DealOpen");
            entity.Property(x => x.DealOpen_InstanceGap_Percent_Default).HasColumnType("decimal(9,4)").HasColumnName("InstanceGap_DealOpen");
            entity.Property(x => x.DealClose_TakeProfit_Percent_Default).HasColumnType("decimal(9,4)").HasColumnName("TakeProfit_DealClose");

            #endregion


            #region COST AVERAGING
            entity.Property(x => x.CstAve_ScalingAmount).HasColumnType("decimal(6,2)");
            #endregion


            #region DYNAMIC BEHAVIOUR
            // No need to configure
            #endregion


            #region PROFIT
            entity.Property(x => x.Leverage_Factor).HasColumnType("decimal(5,2)");
            entity.Property(x => x.ProfitReinvestment_Percent).HasColumnType("decimal(5,2)");
            #endregion


            #region OTHERS

            #region Market Specific
            entity.Property(x => x.QuoteCurrencyAsProductID).HasMaxLength(50);
            entity.Property(x => x.BaseDealOpenPrice).HasColumnType("decimal(24,8)");
            #endregion


            #region DEAL OPEN
            entity.Property(x => x.DealOpen_OffsetPercentToMarket).HasColumnType("decimal(9,4)");
            #endregion


            #region DEAL CLOSE

            entity.Property(x => x.DealClose_OffsetPercentToMarket).HasColumnType("decimal(9,4)");

            #endregion


            #region Delayed Order Trigger
            entity.Property(x => x.DelayedOrderTrigger_PercentRange).HasColumnType("decimal(9,4)");
            #endregion


            #endregion


            #region EXCHANGE CONFIGS
            entity.Property(x => x.TradeExchange).HasMaxLength(100);
            entity.Property(x => x.TradingFee_Percent).HasColumnType("decimal(6,4)");
            entity.Property(x => x.TradeExchangeConfigurationID).HasMaxLength(50);
            #endregion


            #region POSITION TRACKING
            entity.Property(x => x.PosTracking_DealOpen_Active).HasColumnName("DealOpen_Active");
            entity.Property(x => x.PosTracking_DealOpen_TotalPosSize_Max_Calibrated).HasColumnType("decimal(24,8)").HasColumnName("DealOpen_TotalPosSize_Max_Calibrated");
            entity.Property(x => x.PosTracking_DealOpen_Price_MinOffSetPercent).HasColumnType("decimal(8,4)").HasColumnName("DealOpen_Price_MinOffSetPercent");

            entity.Property(x => x.PosTracking_DealClose_OffSetMethod).HasColumnName("DealClose_OffSetMethod");
            entity.Property(x => x.PosTracking_DealClose_TotalPosSize_Min_Calibrated).HasColumnType("decimal(24,8)").HasColumnName("DealClose_TotalPosSize_Min_Calibrated");
            entity.Property(x => x.PosTracking_DealClose_Price_MinOffSetPercent).HasColumnType("decimal(8,4)").HasColumnName("DealClose_Price_MinOffSetPercent");
            #endregion


            #region VERSION INFO

            entity.OwnsOne(
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
                                sa.ToTable("Bot_99_VersionInfo");

                            }
                            );

            #endregion
        }


        private void SplitSectionsToTables(EntityTypeBuilder<Bot> entity)
        {

            #region DYNAMIC BEHAVIOUR

            entity.SplitToTable("Bot_02_DynamicBehaviour", SchemaName,
            tableBuilder =>
            {
                tableBuilder.Property(p => p.TradeDirection);
                tableBuilder.Property(p => p.OrderTrigger_DealOpen);
                tableBuilder.Property(p => p.OrderTrigger_DealClose);
                tableBuilder.Property(p => p.OrderExecution_DealOpen);
                tableBuilder.Property(p => p.OrderExecution_DealClose);
                tableBuilder.Property(p => p.DealOpen_OffsetPercentToMarket);
                tableBuilder.Property(p => p.DealClose_OffsetPercentToMarket);

                tableBuilder.Property(p => p.DealOpen_MinRank_RelToPriceLimit).HasColumnName("MinRank_DealOpen");
            }
            );

            #endregion


            #region COST AVERAGING
            entity.SplitToTable("Bot_03_PosSizeTracking", SchemaName,
            tableBuilder =>
            {
                tableBuilder.Property(p => p.PosTracking_DealClose_OffSetMethod);
                tableBuilder.Property(p => p.PosTracking_DealClose_Price_MinOffSetPercent);
                tableBuilder.Property(p => p.PosTracking_DealClose_TotalPosSize_Min_Calibrated);

                tableBuilder.Property(p => p.PosTracking_DealOpen_Active);
                tableBuilder.Property(p => p.PosTracking_DealOpen_TotalPosSize_Max_Calibrated);
                tableBuilder.Property(p => p.PosTracking_DealOpen_Price_MinOffSetPercent);
            }
            );
            #endregion


            #region POSITION TRACKING
            entity.SplitToTable("Bot_04_CostAveraging", SchemaName,
                        tableBuilder =>
                        {
                            tableBuilder.Property(p => p.CstAve_Method).HasColumnName("Method");
                            tableBuilder.Property(p => p.CstAve_Rndng_PositionAmount).HasColumnName("Rndng_PositionAmount");
                            tableBuilder.Property(p => p.CstAve_Rndng_Volume).HasColumnName("Rndng_Volume");
                            tableBuilder.Property(p => p.CstAve_ScalingAmount).HasColumnName("ScalingAmount");

                        }
                        );
            #endregion


            #region PROFIT

            entity.SplitToTable("Bot_05_Profit", SchemaName,
                                tableBuilder =>
                                {
                                    tableBuilder.Property(p => p.MarketCategory).HasColumnName("MarketCategory");
                                    tableBuilder.Property(p => p.Leverage_Factor).HasColumnName("Leverage_Factor");
                                    tableBuilder.Property(p => p.ProfitReInvestment_Type).HasColumnName("ProfitReInvestment_Type");
                                    tableBuilder.Property(p => p.ProfitReinvestment_Percent).HasColumnName("ProfitReinvestment_Percent");
                                    tableBuilder.Property(p => p.DealsTracking_Active).HasColumnName("DealsTracking_Active");
                                    tableBuilder.Property(p => p.PartialTP_Volume_Rounding).HasColumnName("PartialTP_Volume_Rounding");
                                    tableBuilder.Property(p => p.PartialTP_RemainderAllocation).HasColumnName("PartialTP_RemainderAllocation");
                                }
                                );

            #endregion


            #region OTHERS

            entity.SplitToTable("Bot_06_Others", SchemaName,
                                tableBuilder =>
                                {
                                    #region Basic
                                    tableBuilder.Property(p => p.TradeTimeSpan).HasColumnName("TradeTimeSpan");
                                    #endregion

                                    #region Market Specific
                                      tableBuilder.Property(p => p.QuoteCurrencyAsProductID).HasColumnName("QuoteCurrencyAsProductID");
                                    tableBuilder.Property(p => p.BaseDealOpenPrice).HasColumnName("BaseDealOpenPrice");
                                    #endregion



                                    #region DEAL CLOSE

                                    tableBuilder.Property(p => p.DealClose_MinRank_RelToMarket).HasColumnName("DealClose_MinRank_RelToMarket");
                                    #endregion


                                    #region Delayed Order Trigger
                                    tableBuilder.Property(p => p.DelayedOrderTrigger_Active).HasColumnName("DelayedOrderTrigger_Active");
                                    tableBuilder.Property(p => p.DelayedOrderTrigger_PercentRange).HasColumnName("DelayedOrderTrigger_PercentRange");
                                    tableBuilder.Property(p => p.DelayedOrderTrigger_ImmediateOrdersCount).HasColumnName("DelayedOrderTrigger_ImmediateOrdersCount");
                                    #endregion

                                }
                                );

            #endregion


            #region EXCHANGE CONFIG
            entity.SplitToTable("Bot_07_ExchangeConfig", SchemaName,
                        tableBuilder =>
                        {
                            tableBuilder.Property(p => p.TradeExchange);
                            tableBuilder.Property(p => p.TradingFee_Percent);
                            tableBuilder.Property(p => p.TradeExchangeConfigurationID);
                            tableBuilder.Property(p => p.Volume_BatchUnits);
                        }
                        );

            #endregion

        }
    }

}