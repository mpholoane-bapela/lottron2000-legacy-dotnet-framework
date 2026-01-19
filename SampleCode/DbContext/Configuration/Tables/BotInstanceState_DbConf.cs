using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class BotInstanceState_DbTableConfiguration : SchemaLogic_DbTableConfiguration_Base
{

    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<BotInstanceState>();
        entity.ToTable("BotInstanceState", SchemaName);

        #region DB ID
        modelBuilder.Entity<BotInstanceState>().HasKey(p => p.BotInstanceStateID);

        entity.Property(x => x.DbID).HasColumnName("DbID");
        entity.Property(x => x.DbID).HasColumnType("int");

        const int DbID_SEED = 1;
        const int DbID_INCREMENT = 1;

        modelBuilder.Entity<BotInstanceState>()
             .Property(p => p.DbID)
             .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

        modelBuilder.Entity<BotInstanceState>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        #endregion

        #region Column Lengths
        entity.Property(x => x.BotInstanceID).HasMaxLength(50);
        entity.Property(x => x.BotInstanceStateID).HasMaxLength(50);
        entity.Property(x => x.ApiOrderID_Current).HasMaxLength(50);
        entity.Property(x => x.ApiOrderID_DealOpen).HasMaxLength(50);
        entity.Property(x => x.ApiOrderID_DealClose).HasMaxLength(50);
        entity.Property(x => x.ApiTimeStamp_Current).HasMaxLength(100);
        entity.Property(x => x.ApiTimeStamp_DealOpen).HasMaxLength(100);
        entity.Property(x => x.ApiTimeStamp_DealClose).HasMaxLength(100);

        entity.Property(x => x.DealID).HasMaxLength(100);
        #endregion

        //----------------------------------------------------------------------------------

        #region VersionInfo

        modelBuilder.Entity<BotInstanceState>().OwnsOne(
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
