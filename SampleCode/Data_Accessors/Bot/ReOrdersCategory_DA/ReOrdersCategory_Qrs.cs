using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersCategoryQueries
{
    private IGenericDbRepository _genericRepository;


    public ReOrdersCategoryQueries(IGenericDbRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<List<ReOrdersCategory_DTO>> GetAll()
    {
        var itemsQuery = GetAllDTOsQuery();
        return await itemsQuery.ToListAsync();

    }

    public async Task<List<ReOrdersCategory>> GetAllEntities()
    {
        var itemsQuery = GetAllEntitiesQuery();
        return await itemsQuery.ToListAsync();

    }

    private IQueryable<ReOrdersCategory> GetAllEntitiesQuery()
    {
        var itemsQuery = _genericRepository.GetAll<ReOrdersCategory>()
                    .AsNoTracking();

        return itemsQuery;
    }

    private IQueryable<ReOrdersCategory_DTO> GetAllDTOsQuery()
    {
        var itemsQuery = _genericRepository.GetAll<ReOrdersCategory>()
            .AsNoTracking()
            .Select(r => new ReOrdersCategory_DTO
            {
                Category = r.Category,
                PriceReference = r.PriceReference,
                AmountOfPosition_Estimate = r.AmountOfPosition_Estimate,
                DealClose_StartOffSet = r.DealClose_StartOffSet,
                DealClose_SpecificStartPrice = r.DealClose_SpecificStartPrice,
                NumberOfOrders = r.NumberOfOrders,
                OrdersGap_Percent = r.OrdersGap_Percent,
                OverallPriority = r.OverallPriority,
                DoPlaceOrders = r.DoPlaceOrders,
                VersionInfo = r.VersionInfo,
                ReOrdersCategoryID = r.ReOrdersCategoryID,
            });

        return itemsQuery;
    }

}