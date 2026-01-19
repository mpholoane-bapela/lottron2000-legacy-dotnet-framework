using Microsoft.EntityFrameworkCore;

namespace TradingBots.Native.Infra.DbAccess;

public static class DbModelsConfigurator
{
    public static void ConfigureAll(ModelBuilder modelBuilder)
    {
        Bot_DbTableConfiguration Bot_DbTableConfiguration = new Bot_DbTableConfiguration();
        Bot_DbTableConfiguration.ConfigureModel(modelBuilder);

        BotInstanceInstance_DbTableConfiguration BotInstanceInstance_DbTableConfiguration = new BotInstanceInstance_DbTableConfiguration();
        BotInstanceInstance_DbTableConfiguration.ConfigureModel(modelBuilder);

        BotInstanceState_DbTableConfiguration BotInstanceStateState_DbTableConfiguration = new BotInstanceState_DbTableConfiguration();
        BotInstanceStateState_DbTableConfiguration.ConfigureModel(modelBuilder);

        BotPartialTP_DbConfig BotPartialTP_DbConfig = new BotPartialTP_DbConfig();
        BotPartialTP_DbConfig.ConfigureModel(modelBuilder);

        GlobalStopLoss_DbConfig GlobalStopLoss_DbConfig = new GlobalStopLoss_DbConfig();
        GlobalStopLoss_DbConfig.ConfigureModel(modelBuilder);

        CompletedDeal_DbConfig CompletedDeal_DbConfig = new CompletedDeal_DbConfig();
        CompletedDeal_DbConfig.ConfigureModel(modelBuilder);


        RecommAction_DbConf RecommAction_DbConf = new RecommAction_DbConf();
        RecommAction_DbConf.ConfigureModel(modelBuilder);

        Filters_DbConf Filters_DbConf = new Filters_DbConf();
        Filters_DbConf.ConfigureModel(modelBuilder);



        ReOrdersMain_DbConfig ReOrdersMain_DbConfig = new ReOrdersMain_DbConfig();
        ReOrdersMain_DbConfig.ConfigureModel(modelBuilder);


        ReOrdersCategory_DbConfig ReOrdersCategory_DbConfig = new ReOrdersCategory_DbConfig();
        ReOrdersCategory_DbConfig.ConfigureModel(modelBuilder);
    }
}