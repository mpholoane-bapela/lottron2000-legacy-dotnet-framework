using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;


namespace TradingBots.Native.Infra.DbAccess;

public class BotPartialTPQueries
{
    private IGenericDbRepository _genericRepository;


    public BotPartialTPQueries(IGenericDbRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }


    public async Task<List<BotPartialTP>> GetAllEntities()
    {
        var itemsQuery = GetAllEntitiesQuery();
        return await itemsQuery.ToListAsync();

    }


    private IQueryable<BotPartialTP> GetAllEntitiesQuery()
    {
        var itemsQuery = _genericRepository.GetAll<BotPartialTP>()
                    .AsNoTracking();

        return itemsQuery;
    }


    private IQueryable<PartialTP_DTO> GetAllDTOsQuery()
    {
        var itemsQuery = _genericRepository.GetAll<BotPartialTP>()
            .AsNoTracking()
            .Select(r => new PartialTP_DTO
            {
                BotInstanceID = r.BotInstanceID,
                Priority_Partial = r.Priority_Partial,
                Priority_Instance = r.Priority_Instance,
                Volume_CalcParameter_Percent = r.Volume_CalcParameter_Percent,
                Volume_CalcResult_Amount = r.Volume_CalcResult_Amount,

                ReferencePoint_Type = r.ReferencePoint_Type,
                TakeProfit_Percent = r.TakeProfit_Percent,
                PartialWorkFlowStatus = r.PartialTpStatus,
                PrevState = r.PrevState,
                CurrentState = r.CurrentState,
                DealID = r.DealID,
                ApiOrderID_Current = r.ApiOrderID_Current,
                ApiOrderID_DealOpen = r.ApiOrderID_DealOpen,
                ApiOrderID_DealClose = r.ApiOrderID_DealClose,
                ApiTimeStamp_Current = r.ApiTimeStamp_Current,
                ApiTimeStamp_DealOpen = r.ApiTimeStamp_DealOpen,
                ApiTimeStamp_DealClose = r.ApiTimeStamp_DealClose,
                Active = r.Active,
                Volume_ActualClosed = r.Volume_ActualClosed,
                Profit_EstAmount = r.Profit_EstAmount,
                VersionInfo = r.VersionInfo,
                BotPartialTPID = r.BotPartialTPID,

                #region COPIED
                DealClose_Price_Initial = r.DealClose_Price_Initial,
                DealClose_Price_Initial_Relevant = r.DealClose_Price_Initial_Relevant,
                DealClose_Price_Final = r.DealClose_Price_Final,

                // TTP
                TTP_Active = r.TTP_Active,
                TTP_Attempts_Max = r.TTP_Attempts_Max,
                TTP_Attempts_Current = r.TTP_Attempts_Current,

                TTP_ActivationPoint_OffSetPercent = r.TTP_ActivationPoint_OffSetPercent,
                TTP_TriggerPoint_OffSetPercent = r.TTP_TriggerPoint_OffSetPercent,
                TTP_EventualTrigger_Price = r.TTP_EventualPrice_Trigger,
                TTP_OrderPoint_OffSetPercent = r.TTP_OrderPoint_OffSetPercent,
                TTP_EventualPrice_DealClose = r.TTP_EventualPrice_DealClose,

                OrderTrigger_DealClose = r.OrderTrigger_DealClose,
                OrderExecution_DealClose = r.OrderExecution_DealClose,
                DealClose_OffsetPercentToMarket = r.DealClose_OffsetPercentToMarket,

                PosTracking_DealClose_OffSetMethod = r.PosTracking_DealClose_OffSetMethod,
                PosTracking_DealClose_Price_MinOffSetPercent = r.PosTracking_DealClose_Price_MinOffSetPercent,

                #endregion
            });

        return itemsQuery;
    }

}