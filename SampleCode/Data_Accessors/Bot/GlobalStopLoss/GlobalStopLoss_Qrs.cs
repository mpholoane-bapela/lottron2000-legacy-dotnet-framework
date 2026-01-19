using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class GlobalStopLossQueries
{
    private IGenericDbRepository _genericRepository;


    public GlobalStopLossQueries(IGenericDbRepository genericRepository)
    { _genericRepository = genericRepository; }


    public async Task<List<GlobalStopLoss>> GetAllEntities()
    {
        var itemsQuery = GetAllEntitiesQuery();
        return await itemsQuery.ToListAsync();
    }


    private IQueryable<GlobalStopLoss> GetAllEntitiesQuery()
    {
        var itemsQuery = _genericRepository.GetAll<GlobalStopLoss>().AsNoTracking();

        return itemsQuery;
    }


    private IQueryable<GlobalStopLoss_DTO> GetAllDTOsQuery()
    {
        var itemsQuery = _genericRepository.GetAll<GlobalStopLoss>()
            .AsNoTracking()
            .Select(r => new GlobalStopLoss_DTO
            {
                GlobalStopLossID = r.GlobalStopLossID,
                Priority = r.Priority,
                Active = r.Active,
                StopLossType = r.StopLossType,
                CircuitBreaker_Active = r.CircuitBreaker_Active,
                CircuitBreaker_TimeOut_Seconds = r.CircuitBreaker_TimeOut_Seconds,
                CircuitBreaker_TimeOut_StartTime = r.CircuitBreaker_TimeOut_StartTime,
                CircuitBreaker_TimeOut_EndTime = r.CircuitBreaker_TimeOut_EndTime,
                ActivationPoint_OffSetPercent = r.ActivationPoint_OffSetPercent,
                Activation_CalculatedPrice = r.Activation_CalculatedPrice,
                TakeLossPoint_OffSetPercent = r.TakeLossPoint_OffSetPercent,
                TakeLoss_CalculatedPrice = r.TakeLoss_CalculatedPrice,
                TakeLoss_FinalPrice = r.TakeLoss_FinalPrice,
                StopLossReference = r.StopLossReference,
                Volume_Calculated = r.Volume_Calculated,
                NumberOfRuns_Max = r.NumberOfRuns_Max,
                NumberOfRuns_Current = r.NumberOfRuns_Current,
                PositionAmount_Percent = r.PositionAmount_Percent,
                PositionAmount_Reserved = r.PositionAmount_Reserved,
                WorkflowStatus = r.WorkflowStatus,
                DealID = r.DealID,
                ApiOrderID = r.ApiOrderID,
                ApiTimeStamp_Current = r.ApiTimeStamp_Current,
                ApiTimeStamp_DealOpen = r.ApiTimeStamp_DealOpen,
                ApiTimeStamp_DealClose = r.ApiTimeStamp_DealClose,
                VersionInfo = r.VersionInfo,
                BotID = r.BotID
            });

        return itemsQuery;
    }
}