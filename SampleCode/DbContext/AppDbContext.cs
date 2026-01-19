using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class AppDbContext : DbContext_AlexisBase
    {
        #region TradingBots.Logic
        public DbSet<Bot> logic__Bot { get; set; }
        public DbSet<BotInstance> logic__BotInstanceInstance { get; set; }
        public DbSet<BotInstanceState> logic__BotInstanceState { get; set; }

        public DbSet<BotPartialTP> logic__BotPartialTP { get; set; }
        public DbSet<GlobalStopLoss> logic__GlobalStopLoss { get; set; }


        public DbSet<CompletedDeal> logic__CompletedDeal { get; set; }

        public DbSet<RecommendationFilter> logic__RecommendationFilter { get; set; }
        public DbSet<RecommendationAction> logic__RecommendationAction { get; set; }

        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbModelsConfigurator.ConfigureAll(modelBuilder);
        }

        public override void Dispose()
        {
            int x = 0;
        }
    }

}
