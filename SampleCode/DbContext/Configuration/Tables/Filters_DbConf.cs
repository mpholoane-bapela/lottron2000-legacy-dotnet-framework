using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class Filters_DbConf : SchemaLogic_DbTableConfiguration_Base
    {

        public void ConfigureModel(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<RecommendationFilter>();
            entity.ToTable("RecommendationFilter", SchemaName);

            #region DB ID
            modelBuilder.Entity<RecommendationFilter>().HasKey(p => p.RecommendationFilterID);

            entity.Property(x => x.DbID).HasColumnName("DbID");
            entity.Property(x => x.DbID).HasColumnType("int");

            const int DbID_SEED = 1;
            const int DbID_INCREMENT = 1;

            modelBuilder.Entity<RecommendationFilter>()
                 .Property(p => p.DbID)
                 .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);

            modelBuilder.Entity<RecommendationFilter>().Property(u => u.DbID).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            #endregion

            #region Column Lengths
            entity.Property(x => x.RecommendationFilterID).HasMaxLength(50);
            entity.Property(x => x.TradingPair_Domain).HasMaxLength(50);
            entity.Property(x => x.SourceData_TradeExchange).HasMaxLength(100);
            entity.Property(x => x.AttributeToFilter).HasMaxLength(100);
            entity.Property(x => x.ValueAsString).HasMaxLength(250);
            #endregion

            //----------------------------------------------------------------------------------

            #region VersionInfo

            modelBuilder.Entity<RecommendationFilter>().OwnsOne(
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

