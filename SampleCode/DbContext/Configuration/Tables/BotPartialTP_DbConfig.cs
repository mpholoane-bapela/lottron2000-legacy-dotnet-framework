using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class BotPartialTP_DbConfig : SchemaLogic_DbTableConfiguration_Base
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BotPartialTP>();
        entity.ToTable("BotPartialTP_00_Root", SchemaName).HasOne<BotPartialTP>().WithOne().HasForeignKey<BotPartialTP>(a => a.BotPartialTPID);


        #region DB ID
        entity.Property(x => x.BotPartialTPID).HasMaxLength(50);
        modelBuilder.Entity<BotPartialTP>().HasKey(p => p.BotPartialTPID);


        entity.Property(x => x.BotInstanceID).HasMaxLength(50);

        entity.Property(x => x.DbID).HasColumnName("DbID");
        entity.Property(x => x.DbID).HasColumnType("int");

        const int DbID_SEED = 1;
        const int DbID_INCREMENT = 1;

        modelBuilder.Entity<BotPartialTP>()
             .Property(p => p.DbID)
             .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

        modelBuilder.Entity<BotPartialTP>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        #endregion

        //----------------------------------------------------------------------------------

        SetProperties(entity);
        SplitSectionsToTables(entity);
    }


    private void SetProperties(EntityTypeBuilder<BotPartialTP> entity)
    {
        #region ROOT

        #endregion


        #region MAIN

        entity.Property(x => x.Volume_CalcParameter_Percent).HasColumnType("decimal(9,4)");
        entity.Property(x => x.Volume_CalcResult_Amount).HasColumnType("decimal(24,8)");

        entity.Property(x => x.TakeProfit_Percent).HasColumnType("decimal(9,4)");
        entity.Property(x => x.DealClose_OffsetPercentToMarket).HasColumnType("decimal(9,4)");
        #endregion


        #region STATE
        entity.Property(x => x.DealID).HasMaxLength(50);
        entity.Property(x => x.ApiOrderID_Current).HasMaxLength(50);
        entity.Property(x => x.ApiOrderID_DealOpen).HasMaxLength(50);
        entity.Property(x => x.ApiOrderID_DealClose).HasMaxLength(50);
        entity.Property(x => x.ApiTimeStamp_Current).HasMaxLength(50);
        entity.Property(x => x.ApiTimeStamp_DealOpen).HasMaxLength(50);
        entity.Property(x => x.ApiTimeStamp_DealClose).HasMaxLength(50);

        entity.Property(x => x.DealClose_Price_Initial).HasColumnType("decimal(9,4)");
        entity.Property(x => x.DealClose_Price_Initial_Relevant).HasColumnType("decimal(9,4)");
        entity.Property(x => x.DealClose_Price_Final).HasColumnType("decimal(9,4)");
        entity.Property(x => x.Volume_ActualClosed).HasColumnType("decimal(9,4)");
        entity.Property(x => x.Profit_EstAmount).HasColumnType("decimal(9,4)");
        #endregion


        #region TRAILING TAKE PROFIT
        entity.Property(x => x.TTP_ActivationPoint_OffSetPercent).HasColumnType("decimal(9,4)");
        entity.Property(x => x.TTP_TriggerPoint_OffSetPercent).HasColumnType("decimal(9,4)");
        entity.Property(x => x.TTP_EventualPrice_Trigger).HasColumnType("decimal(24,8)");
        entity.Property(x => x.TTP_OrderPoint_OffSetPercent).HasColumnType("decimal(9,4)");
        entity.Property(x => x.TTP_EventualPrice_DealClose).HasColumnType("decimal(24,8)");
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
                            sa.ToTable("BotPartialTP_00_Root");
                        }
                        );

        #endregion
    }


    private void SplitSectionsToTables(EntityTypeBuilder<BotPartialTP> entity)
    {
        #region MAIN
        entity.SplitToTable("BotPartialTP_01_Main", SchemaName,
        tableBuilder =>
        {
            tableBuilder.Property(p => p.Priority_Partial);
            tableBuilder.Property(p => p.Priority_Instance);

            tableBuilder.Property(p => p.Volume_CalcParameter_Percent);
            tableBuilder.Property(p => p.Volume_CalcResult_Amount);
            tableBuilder.Property(p => p.ReferencePoint_Type);
            tableBuilder.Property(p => p.TakeProfit_Percent);
            tableBuilder.Property(p => p.Active);
            tableBuilder.Property(p => p.OrderTrigger_DealClose);
            tableBuilder.Property(p => p.OrderExecution_DealClose);
            tableBuilder.Property(p => p.DealClose_OffsetPercentToMarket);
        }
        );

        #endregion


        #region TRAILING TAKE PROFIT
        entity.SplitToTable("BotPartialTP_02_TrlngTP", SchemaName,
        tableBuilder =>
        {
            tableBuilder.Property(p => p.Priority_Partial);
            tableBuilder.Property(p => p.Priority_Instance);
            tableBuilder.Property(p => p.TTP_Active).HasColumnName("Active");
            tableBuilder.Property(p => p.TTP_ActivationPoint_OffSetPercent).HasColumnName("ActivationPoint_OffSetPercent");
            tableBuilder.Property(p => p.TTP_TriggerPoint_OffSetPercent).HasColumnName("TriggerPoint_OffSetPercent");
            tableBuilder.Property(p => p.TTP_OrderPoint_OffSetPercent).HasColumnName("OrderPoint_OffSetPercent");
            tableBuilder.Property(p => p.TTP_EventualPrice_Trigger).HasColumnName("EventualPrice_Trigger");
            tableBuilder.Property(p => p.TTP_EventualPrice_DealClose).HasColumnName("EventualPrice_DealClose");
            tableBuilder.Property(p => p.TTP_Attempts_Max).HasColumnName("Attempts_Max");
            tableBuilder.Property(p => p.TTP_Attempts_Current).HasColumnName("Attempts_Current");
        }
        );

        #endregion


        #region STATE

        entity.SplitToTable("BotPartialTP_03_State", SchemaName,
        tableBuilder =>
        {
            tableBuilder.Property(p => p.Priority_Partial);
            tableBuilder.Property(p => p.Priority_Instance);
            tableBuilder.Property(p => p.PartialTpStatus);
            tableBuilder.Property(p => p.PrevState);
            tableBuilder.Property(p => p.CurrentState);
            tableBuilder.Property(p => p.DealID);
            tableBuilder.Property(p => p.ApiOrderID_Current);
            tableBuilder.Property(p => p.ApiOrderID_DealOpen);
            tableBuilder.Property(p => p.ApiOrderID_DealClose);
            tableBuilder.Property(p => p.ApiTimeStamp_Current);
            tableBuilder.Property(p => p.ApiTimeStamp_DealOpen);
            tableBuilder.Property(p => p.ApiTimeStamp_DealClose);
            tableBuilder.Property(p => p.DealClose_Price_Initial);
            tableBuilder.Property(p => p.DealClose_Price_Initial_Relevant);
            tableBuilder.Property(p => p.DealClose_Price_Final);
            tableBuilder.Property(p => p.Volume_ActualClosed);
            tableBuilder.Property(p => p.Profit_EstAmount);
        }
        );

        #endregion


        #region POSITION SIZE TRACKING
        entity.SplitToTable("BotPartialTP_04_PosSizeTrck", SchemaName,
        tableBuilder =>
        {
            tableBuilder.Property(p => p.Priority_Partial);
            tableBuilder.Property(p => p.Priority_Instance);
            tableBuilder.Property(p => p.PosTracking_DealClose_OffSetMethod).HasColumnName("DealClose_OffSetMethod");
            tableBuilder.Property(p => p.PosTracking_DealClose_Price_MinOffSetPercent).HasColumnName("DealClose_Price_MinOffSetPercent");
        }
        );
        #endregion

    }
}