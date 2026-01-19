using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersMain_DbConfig : SchemaLogic_DbTableConfiguration_Base
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ReOrdersMain>();
        entity.ToTable("ReOrdersMain", SchemaName);

        #region DB ID
        modelBuilder.Entity<ReOrdersMain>().HasKey(p => p.ReOrdersMainID);

        entity.Property(x => x.DbID).HasColumnName("DbID");
        entity.Property(x => x.DbID).HasColumnType("int");

        const int DbID_SEED = 1;
        const int DbID_INCREMENT = 1;

        modelBuilder.Entity<ReOrdersMain>()
             .Property(p => p.DbID)
             .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

        modelBuilder.Entity<ReOrdersMain>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        #endregion

        #region Column Lengths
        entity.Property(x => x.ReOrdersMainID).HasMaxLength(50);
        entity.Property(x => x.PositionLimit).HasColumnType("decimal(24,8)");


        #endregion

        //----------------------------------------------------------------------------------

        #region VersionInfo

        modelBuilder.Entity<ReOrdersMain>().OwnsOne(
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