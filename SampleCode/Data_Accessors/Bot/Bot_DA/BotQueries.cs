using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class BotQueries
{
    private IGenericDbRepository _genericRepository;

    public BotQueries(IGenericDbRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public List<Bot_DTO> GetAll()
    {
        return GetAllQuery().ToList();
    }

    public async Task<List<Bot>> GetAllEntitiesAsync(bool? active = null)
    {
        var items = GetAllEntitiesQuery();


        if (active != null)
        {
            items = items.Where(a => a.Active == active);
        }

        return await items.ToListAsync();
    }


    public async Task<List<Bot_DTO>> GetAllAsync(bool? active = null, string tradeExchange = null)
    {
        var items = GetAllQuery();

        if (active != null)
        {
            items = items.Where(a => a.Active == active);
        }

        if (tradeExchange != null)
        {
            items = items.Where(a => a.TradeExchange == tradeExchange);
        }

        return await items.ToListAsync();
    }


    #region BOT GRAPH
    // *** HEAVY DB CALL
    public async Task<BotEntitiesDTOs> GetBotGraph()
    {
        var item = await GetAllGraphsQuery().FirstOrDefaultAsync();
        return MapGraph__Bot_ViewDTO(item);
    }


    private BotEntitiesDTOs MapGraph__Bot_ViewDTO(Bot botEntity)
    {
        #region CORE
        Bot_DTO bot_DTO = new Bot_DTO();

        bot_DTO.Active = botEntity.Active;
        bot_DTO.BaseDealOpenPrice = botEntity.BaseDealOpenPrice;
        bot_DTO.BotID = botEntity.BotID;
        bot_DTO.OrderExecution_DealOpen = botEntity.OrderExecution_DealOpen;
        bot_DTO.OrderExecution_DealClose = botEntity.OrderExecution_DealClose;


        bot_DTO.OrderTrigger_DealOpen = botEntity.OrderTrigger_DealOpen;
        bot_DTO.OrderTrigger_DealClose = botEntity.OrderTrigger_DealClose;

        bot_DTO.Budget_TotalForBot = botEntity.Budget_TotalForBot;
        bot_DTO.DealOpen_MinRank_RelToPriceLimit = botEntity.DealOpen_MinRank_RelToPriceLimit;
        bot_DTO.DealOpen_OffsetPercentToMarket = botEntity.DealOpen_OffsetPercentToMarket;

        bot_DTO.DealOpen_InstanceGap_Percent_Default = botEntity.DealOpen_InstanceGap_Percent_Default;

        bot_DTO.DealOpen_PriceLimit_Default = botEntity.DealOpen_PriceLimit_Default;
        bot_DTO.DelayedOrderTrigger_Active = botEntity.DelayedOrderTrigger_Active;

        bot_DTO.DelayedOrderTrigger_ImmediateOrdersCount = botEntity.DelayedOrderTrigger_ImmediateOrdersCount;
        bot_DTO.DelayedOrderTrigger_PercentRange = botEntity.DelayedOrderTrigger_PercentRange;

        bot_DTO.DealsTracking_Active = botEntity.DealsTracking_Active;
        bot_DTO.PartialTP_Volume_Rounding = botEntity.PartialTP_Volume_Rounding;
        bot_DTO.PartialTP_RemainderAllocation = botEntity.PartialTP_RemainderAllocation;

        bot_DTO.QuoteCurrencyAsProductID = botEntity.QuoteCurrencyAsProductID;
        bot_DTO.DealClose_MinRank_RelToMarket = botEntity.DealClose_MinRank_RelToMarket;
        bot_DTO.DealClose_OffsetPercentToMarket = botEntity.DealClose_OffsetPercentToMarket;
        bot_DTO.DealClose_TakeProfit_Percent_Default = botEntity.DealClose_TakeProfit_Percent_Default;

        bot_DTO.CstAve_Method = botEntity.CstAve_Method;
        bot_DTO.CstAve_Rndng_PositionAmount = botEntity.CstAve_Rndng_PositionAmount;
        bot_DTO.CstAve_Rndng_Volume = botEntity.CstAve_Rndng_Volume;
        bot_DTO.CstAve_ScalingAmount = botEntity.CstAve_ScalingAmount;

        bot_DTO.Title = botEntity.Title;
        bot_DTO.TradeExchange = botEntity.TradeExchange;

        bot_DTO.TradeTimeSpan = botEntity.TradeTimeSpan;
        bot_DTO.TradingPair = botEntity.TradingPair;
        bot_DTO.TradeDirection = botEntity.TradeDirection; 
        
        bot_DTO.VersionInfo = botEntity.VersionInfo;

        bot_DTO.ProfitReInvestment_Type = botEntity.ProfitReInvestment_Type;
        bot_DTO.TradingFee_Percent = botEntity.TradingFee_Percent;
        bot_DTO.ProfitReinvestment_Percent = botEntity.ProfitReinvestment_Percent;

        bot_DTO.MarketCategory = botEntity.MarketCategory;
        bot_DTO.Leverage_Factor = botEntity.Leverage_Factor;

        bot_DTO.TradeExchangeConfigurationID = botEntity.TradeExchangeConfigurationID;
        bot_DTO.Volume_BatchUnits = botEntity.Volume_BatchUnits;


        bot_DTO.PosTracking_DealOpen_Active = botEntity.PosTracking_DealOpen_Active;
        bot_DTO.PosTracking_DealOpen_TotalPosSize_Max_Calibrated = botEntity.PosTracking_DealOpen_TotalPosSize_Max_Calibrated;
        bot_DTO.PosTracking_DealOpen_Price_MinOffSetPercent = botEntity.PosTracking_DealOpen_Price_MinOffSetPercent;


        bot_DTO.PosTracking_DealClose_OffSetMethod = botEntity.PosTracking_DealClose_OffSetMethod;
        bot_DTO.PosTracking_DealClose_TotalPosSize_Min_Calibrated = botEntity.PosTracking_DealClose_TotalPosSize_Min_Calibrated;
        bot_DTO.PosTracking_DealClose_Price_MinOffSetPercent = botEntity.PosTracking_DealClose_Price_MinOffSetPercent;

        #endregion


        #region RELATIONAL
        var instancesWrapper = MapGraph__BotInstances_ViewDTOs(botEntity.BotInstances, botEntity.GlobalStopLosses);
        #endregion

        BotEntitiesDTOs graphRoot = new BotEntitiesDTOs
            (bot_DTO, instancesWrapper.BotInstances, instancesWrapper.BotInstanceStates, instancesWrapper.PartialTPs, instancesWrapper.GlobalStopLosses);

        return graphRoot;
    }


    private BotInstanceAndState_TempWrapper MapGraph__BotInstances_ViewDTOs(ICollection<BotInstance> botInstances, ICollection<GlobalStopLoss> globalStopLosses)
    {
        BotInstanceAndState_TempWrapper tempWrapper = new BotInstanceAndState_TempWrapper();

        foreach (var botInstance in botInstances)
        {
            var mappedBotInstance = MapGraph__BotInstance_ViewDTO(botInstance);
            var mappedBotInstanceState = MapGraph__BotInstanceState_ViewDTO(botInstance.BotInstanceState);
            var partialTPs = MapGraph__PartialTPs_DTO(botInstance.PartialTPs);

            tempWrapper.BotInstances.Add(mappedBotInstance);
            tempWrapper.BotInstanceStates.Add(mappedBotInstanceState);
            tempWrapper.PartialTPs.AddRange(partialTPs);
        }

        tempWrapper.GlobalStopLosses = MapGraph__GlobalStopLoss_DTO(globalStopLosses);

        return tempWrapper;
    }


    private Instance_DTO MapGraph__BotInstance_ViewDTO(BotInstance botInstance)
    {
        Instance_DTO instance_DTO = new Instance_DTO();

        #region CORE
        instance_DTO.Active = botInstance.Active;
        instance_DTO.BotID = botInstance.BotID;
        instance_DTO.BotInstanceID = botInstance.BotInstanceID;
        instance_DTO.Budget = botInstance.Budget;
        instance_DTO.BudgetAllocationType = botInstance.BudgetAllocationType;

        instance_DTO.BudgetPercentageOfParent = botInstance.BudgetPercentageOfParent;

        instance_DTO.InstanceGap_Reference = botInstance.InstanceGap_Reference;
        instance_DTO.InstanceGap_Percent = botInstance.InstanceGap_Percent;

        instance_DTO.DealOpen_Price_Final = botInstance.DealOpen_Price_Final;

        instance_DTO.DealOpen_Price_Initial = botInstance.DealOpen_Price_Initial;

        instance_DTO.OrderVolume_Organic = botInstance.OrderVolume_Organic;
        instance_DTO.OrderVolume_Calibrated = botInstance.OrderVolume_Calibrated;
        instance_DTO.Priority = botInstance.Priority;

        instance_DTO.DealOpen_FeeAmount = botInstance.DealOpen_FeeAmount;

        instance_DTO.VersionInfo = botInstance.VersionInfo;
        #endregion

        return instance_DTO;
    }


    private InstanceState_DTO MapGraph__BotInstanceState_ViewDTO(BotInstanceState botInstanceState)
    {
        InstanceState_DTO instanceState_DTO = new InstanceState_DTO();

        instanceState_DTO.Active = botInstanceState.Active;
        instanceState_DTO.ApiOrderID_Current = botInstanceState.ApiOrderID_Current;
        instanceState_DTO.ApiOrderID_DealOpen = botInstanceState.ApiOrderID_DealOpen;
        instanceState_DTO.ApiOrderID_DealClose = botInstanceState.ApiOrderID_DealClose;
        instanceState_DTO.DealID = botInstanceState.DealID;

        instanceState_DTO.ApiTimeStamp_Current = botInstanceState.ApiTimeStamp_Current;
        instanceState_DTO.ApiTimeStamp_DealOpen = botInstanceState.ApiTimeStamp_DealOpen;
        instanceState_DTO.ApiTimeStamp_DealClose = botInstanceState.ApiTimeStamp_DealClose;

        instanceState_DTO.BotInstanceID = botInstanceState.BotInstanceID;
        instanceState_DTO.BotID = botInstanceState.BotInstance.BotID;
        instanceState_DTO.BotInstanceStateID = botInstanceState.BotInstanceStateID;

        instanceState_DTO.CurrentState = botInstanceState.CurrentState;
        instanceState_DTO.PrevState = botInstanceState.PrevState;
        instanceState_DTO.TradeWorkflowStatus = botInstanceState.TradeWorkflowStatus;

        instanceState_DTO.VersionInfo = botInstanceState.VersionInfo;

        return instanceState_DTO;
    }


    private List<PartialTP_DTO> MapGraph__PartialTPs_DTO(ICollection<BotPartialTP> dbEntities)
    {

        List<PartialTP_DTO> mappedDTOs = new();

        foreach (var dbEntity in dbEntities)
        {
            PartialTP_DTO dto = new PartialTP_DTO();


            //31 proerties

            dto.Active = dbEntity.Active;

            //6
            dto.ApiOrderID_Current = dbEntity.ApiOrderID_Current;
            dto.ApiOrderID_DealClose = dbEntity.ApiOrderID_DealClose;
            dto.ApiOrderID_DealOpen = dbEntity.ApiOrderID_DealOpen;
            dto.ApiTimeStamp_Current = dbEntity.ApiTimeStamp_Current;
            dto.ApiTimeStamp_DealClose = dbEntity.ApiTimeStamp_DealClose;
            dto.ApiTimeStamp_DealOpen = dbEntity.ApiTimeStamp_DealOpen;

            dto.BotInstanceID = dbEntity.BotInstanceID;
            dto.BotPartialTPID = dbEntity.BotPartialTPID;
            dto.CurrentState = dbEntity.CurrentState;

            dto.DealClose_Price_Final = dbEntity.DealClose_Price_Final;
            dto.DealClose_Price_Initial = dbEntity.DealClose_Price_Initial;
            dto.DealClose_Price_Initial_Relevant = dbEntity.DealClose_Price_Initial_Relevant;

            //6
            dto.DealID = dbEntity.DealID;
            dto.PartialWorkFlowStatus = dbEntity.PartialTpStatus;
            dto.PrevState = dbEntity.PrevState;
            dto.Priority_Partial = dbEntity.Priority_Partial;
            dto.Priority_Instance = dbEntity.Priority_Instance;
            dto.Profit_EstAmount = dbEntity.Profit_EstAmount;
            dto.TakeProfit_Percent = dbEntity.TakeProfit_Percent;

            //8
            dto.TTP_Active = dbEntity.TTP_Active;
            dto.TTP_Attempts_Max = dbEntity.TTP_Attempts_Max;
            dto.TTP_Attempts_Current = dbEntity.TTP_Attempts_Current;

            dto.TTP_ActivationPoint_OffSetPercent = dbEntity.TTP_ActivationPoint_OffSetPercent;
            dto.TTP_OrderPoint_OffSetPercent = dbEntity.TTP_OrderPoint_OffSetPercent;
            dto.TTP_EventualPrice_DealClose = dbEntity.TTP_EventualPrice_DealClose;
            dto.TTP_TriggerPoint_OffSetPercent = dbEntity.TTP_TriggerPoint_OffSetPercent;
            dto.TTP_EventualTrigger_Price = dbEntity.TTP_EventualPrice_Trigger;

            dto.Volume_ActualClosed = dbEntity.Volume_ActualClosed;
            dto.Volume_CalcResult_Amount = dbEntity.Volume_CalcResult_Amount;
            dto.Volume_CalcParameter_Percent = dbEntity.Volume_CalcParameter_Percent;

            dto.OrderTrigger_DealClose = dbEntity.OrderTrigger_DealClose;
            dto.OrderExecution_DealClose = dbEntity.OrderExecution_DealClose;
            dto.DealClose_OffsetPercentToMarket = dbEntity.DealClose_OffsetPercentToMarket;


            dto.PosTracking_DealClose_OffSetMethod = dbEntity.PosTracking_DealClose_OffSetMethod;
            dto.PosTracking_DealClose_Price_MinOffSetPercent = dbEntity.PosTracking_DealClose_Price_MinOffSetPercent;
            
            dto.VersionInfo = dbEntity.VersionInfo;

            mappedDTOs.Add(dto);

        }
        return mappedDTOs;
    }

    private List<GlobalStopLoss_DTO> MapGraph__GlobalStopLoss_DTO(ICollection<GlobalStopLoss> dbEntities)
    {
        List<GlobalStopLoss_DTO> mappedDTOs = new();

        foreach (var dbEntity in dbEntities)
        {
            GlobalStopLoss_DTO dto = new();

            dto.Activation_CalculatedPrice = dbEntity.Activation_CalculatedPrice;
            dto.ActivationPoint_OffSetPercent = dbEntity.ActivationPoint_OffSetPercent;
            dto.Active = dbEntity.Active;
            dto.CircuitBreaker_Active = dbEntity.CircuitBreaker_Active;
            dto.CircuitBreaker_TimeOut_Seconds = dbEntity.CircuitBreaker_TimeOut_Seconds;

            dto.CircuitBreaker_TimeOut_StartTime = dbEntity.CircuitBreaker_TimeOut_StartTime;
            dto.CircuitBreaker_TimeOut_EndTime = dbEntity.CircuitBreaker_TimeOut_EndTime;

            dto.ApiOrderID = dbEntity.ApiOrderID;
            dto.ApiTimeStamp_Current = dbEntity.ApiTimeStamp_Current;
            dto.ApiTimeStamp_DealClose = dbEntity.ApiTimeStamp_DealClose;
            dto.ApiTimeStamp_DealOpen = dbEntity.ApiTimeStamp_DealOpen;
            dto.DealID = dbEntity.DealID;
            dto.GlobalStopLossID = dbEntity.GlobalStopLossID;
            dto.Priority = dbEntity.Priority;

            dto.TakeLoss_CalculatedPrice = dbEntity.TakeLoss_CalculatedPrice;
            dto.StopLossReference = dbEntity.StopLossReference;
            dto.TakeLoss_FinalPrice = dbEntity.TakeLoss_FinalPrice;
            dto.TakeLossPoint_OffSetPercent = dbEntity.TakeLossPoint_OffSetPercent;
            dto.VersionInfo = dbEntity.VersionInfo;
            dto.Volume_Calculated = dbEntity.Volume_Calculated;
            dto.WorkflowStatus = dbEntity.WorkflowStatus;
            dto.BotID = dbEntity.BotID;

            // === Missing Mappings Added Below ===
            dto.StopLossType = dbEntity.StopLossType;
            dto.NumberOfRuns_Max = dbEntity.NumberOfRuns_Max;
            dto.NumberOfRuns_Current = dbEntity.NumberOfRuns_Current;
            dto.PositionAmount_Percent = dbEntity.PositionAmount_Percent;
            dto.PositionAmount_Reserved = dbEntity.PositionAmount_Reserved;

            mappedDTOs.Add(dto);
        }

        return mappedDTOs;
    }
    #endregion


    public async Task<List<Bot_DTO>> GetBotAsync()
    {
        var items = GetAllQuery();

        return await items.ToListAsync();
    }


    private IQueryable<Bot_DTO> GetAllQuery()
    {
        var itemsQuery = _genericRepository.GetAll<Bot>()
            .AsNoTracking()
            .Select(r => new Bot_DTO
            {
                Active = r.Active,
                BaseDealOpenPrice = r.BaseDealOpenPrice,
                BotID = r.BotID,
                Budget_TotalForBot = r.Budget_TotalForBot,
                DealOpen_MinRank_RelToPriceLimit = r.DealOpen_MinRank_RelToPriceLimit,


                OrderExecution_DealOpen = r.OrderExecution_DealOpen,
                OrderExecution_DealClose = r.OrderExecution_DealClose,

                OrderTrigger_DealOpen = r.OrderTrigger_DealOpen,
                OrderTrigger_DealClose = r.OrderTrigger_DealClose,

                DealOpen_OffsetPercentToMarket = r.DealOpen_OffsetPercentToMarket,
                DealOpen_InstanceGap_Percent_Default = r.DealOpen_InstanceGap_Percent_Default,

                DealOpen_PriceLimit_Default = r.DealOpen_PriceLimit_Default,

                TradingPair = r.TradingPair,

                TradeDirection = r.TradeDirection,


                QuoteCurrencyAsProductID = r.QuoteCurrencyAsProductID,
                DealClose_MinRank_RelToMarket = r.DealClose_MinRank_RelToMarket,
                DealClose_OffsetPercentToMarket = r.DealClose_OffsetPercentToMarket,

                DealClose_TakeProfit_Percent_Default = r.DealClose_TakeProfit_Percent_Default,


                #region COST AVERAGING
                CstAve_Method = r.CstAve_Method,
                CstAve_Rndng_Volume = r.CstAve_Rndng_Volume,
                CstAve_Rndng_PositionAmount = r.CstAve_Rndng_PositionAmount,
                CstAve_ScalingAmount = r.CstAve_ScalingAmount,
                #endregion


                Title = r.Title,
                TradeTimeSpan = r.TradeTimeSpan,
                TradeExchange = r.TradeExchange,

                DealsTracking_Active = r.DealsTracking_Active,

                ProfitReInvestment_Type = r.ProfitReInvestment_Type,
                ProfitReinvestment_Percent = r.ProfitReinvestment_Percent,

                TradingFee_Percent = r.TradingFee_Percent,
                PartialTP_Volume_Rounding = r.PartialTP_Volume_Rounding,
                PartialTP_RemainderAllocation = r.PartialTP_RemainderAllocation,

                MarketCategory = r.MarketCategory,
                Leverage_Factor = r.Leverage_Factor,

                TradeExchangeConfigurationID = r.TradeExchangeConfigurationID,
                Volume_BatchUnits = r.Volume_BatchUnits,

                PosTracking_DealOpen_Active = r.PosTracking_DealOpen_Active,
                PosTracking_DealOpen_TotalPosSize_Max_Calibrated = r.PosTracking_DealOpen_TotalPosSize_Max_Calibrated,
                PosTracking_DealOpen_Price_MinOffSetPercent = r.PosTracking_DealOpen_Price_MinOffSetPercent,

                PosTracking_DealClose_OffSetMethod = r.PosTracking_DealClose_OffSetMethod,
                PosTracking_DealClose_TotalPosSize_Min_Calibrated = r.PosTracking_DealClose_TotalPosSize_Min_Calibrated,
                PosTracking_DealClose_Price_MinOffSetPercent = r.PosTracking_DealClose_Price_MinOffSetPercent,

                VersionInfo = r.VersionInfo,
            });

        return itemsQuery;
    }


    // Optimization Opportunity: Write custom SQL.
    // Query returns "ALL" DB data, all tables with all their columns
    private IQueryable<Bot> GetAllGraphsQuery()
    {

        //var itemsQuery = _genericRepository.GetAll<Bot>()
        //                                    .Include(b => b.BotInstances).ThenInclude(bi => bi.PartialTPs)
        //                                    .Include(b => b.BotInstances).ThenInclude(bi => bi.BotInstanceState)
        //                                    .AsNoTracking();


        var itemsQuery = _genericRepository.GetAll<Bot>()
        .Include(b => b.BotInstances.Where(a => a.Active == true)).ThenInclude(bi => bi.PartialTPs.Where(ptp => ptp.Active))
        .Include(b => b.BotInstances.Where(a => a.Active == true)).ThenInclude(bi => bi.BotInstanceState)
        .Include(b => b.GlobalStopLosses)
        .AsNoTracking();

        return itemsQuery;
    }


    private IQueryable<Bot> GetAllEntitiesQuery()
    {
        var itemsQuery = _genericRepository.GetAll<Bot>()
            .AsNoTracking();

        return itemsQuery;
    }

}


public class BotInstanceAndState_TempWrapper
{
    public List<Instance_DTO> BotInstances { get; set; }
    public List<InstanceState_DTO> BotInstanceStates { get; set; }
    public List<PartialTP_DTO> PartialTPs { get; set; }
    public List<GlobalStopLoss_DTO> GlobalStopLosses { get; set; }

    public BotInstanceAndState_TempWrapper()
    {
        BotInstances = new();
        BotInstanceStates = new();
        PartialTPs = new();
        GlobalStopLosses = new();
    }

    public BotInstanceAndState_TempWrapper(List<Instance_DTO> botInstances, List<InstanceState_DTO> botInstancesStates, List<PartialTP_DTO> partialTPs, List<GlobalStopLoss_DTO> globalStopLosses)
    {
        BotInstances = botInstances;
        BotInstanceStates = botInstancesStates;
        PartialTPs = partialTPs;
        GlobalStopLosses = globalStopLosses;
    }
}