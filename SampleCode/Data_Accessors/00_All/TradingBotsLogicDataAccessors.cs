namespace TradingBots.Native.Infra.DbAccess
{
    // wrapper class to have single access/entry point
    public class TradingBotsLogicDataAccessors
    {

        #region MEMBERS
        public BotDA BotDA;
        public BotInstanceDA BotInstanceDA;
        public BotInstanceStateDA BotInstanceStateDA;
        #endregion

        public TradingBotsLogicDataAccessors(BotDA botDA, BotInstanceDA botInstanceDA, BotInstanceStateDA botInstanceStateDA)
        {
            SetAccessors(botDA, botInstanceDA, botInstanceStateDA);
        }

        public void SetAccessors(BotDA botDA, BotInstanceDA botInstanceDA, BotInstanceStateDA botInstanceStateDA)
        {
            BotDA = botDA;
            BotInstanceDA = botInstanceDA;
            BotInstanceStateDA = botInstanceStateDA;
        }

    }
}
