using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersCategory_DbConfig : SchemaLogic_DbTableConfiguration_Base
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ReOrdersCategory>();
        entity.ToTable("ReOrdersCategory", SchemaName);

        #region DB ID
        modelBuilder.Entity<ReOrdersCategory>().HasKey(p => p.ReOrdersCategoryID);

        entity.Property(x => x.DbID).HasColumnName("DbID");
        entity.Property(x => x.DbID).HasColumnType("int");

        const int DbID_SEED = 1;
        const int DbID_INCREMENT = 1;

        modelBuilder.Entity<ReOrdersCategory>()
             .Property(p => p.DbID)
             .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

        modelBuilder.Entity<ReOrdersCategory>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        #endregion

        #region Column Lengths
        entity.Property(x => x.ReOrdersCategoryID).HasMaxLength(50);
        entity.Property(x => x.AmountOfPosition_Estimate).HasColumnType("decimal(24,8)");
entity.Property(x => x.DealClose_StartOffSet).HasColumnType("decimal(10,4)");
entity.Property(x => x.DealClose_SpecificStartPrice).HasColumnType("decimal(24,8)");
entity.Property(x => x.OrdersGap_Percent).HasColumnType("decimal(10,4)");


        #endregion

        //----------------------------------------------------------------------------------

        #region VersionInfo

        modelBuilder.Entity<ReOrdersCategory>().OwnsOne(
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