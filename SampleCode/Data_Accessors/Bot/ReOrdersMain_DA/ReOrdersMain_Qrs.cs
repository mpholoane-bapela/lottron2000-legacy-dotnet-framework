using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class ReOrdersMainQueries
{
    private IGenericDbRepository _genericRepository;


    public ReOrdersMainQueries(IGenericDbRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }


    public async Task<List<ReOrdersMain_DTO>> GetAll()
    {
        var itemsQuery = GetAllDTOsQuery();
        return await itemsQuery.ToListAsync();

    }

    public async Task<List<ReOrdersMain>> GetAllEntities()
    {
        var itemsQuery = GetAllEntitiesQuery();
        return await itemsQuery.ToListAsync();

    }


    private IQueryable<ReOrdersMain> GetAllEntitiesQuery()
    {
        var itemsQuery = _genericRepository.GetAll<ReOrdersMain>()
                    .AsNoTracking();

        return itemsQuery;
    }


    private IQueryable<ReOrdersMain_DTO> GetAllDTOsQuery()
    {
        var itemsQuery = _genericRepository.GetAll<ReOrdersMain>()
            .AsNoTracking()
            .Select(r => new ReOrdersMain_DTO
            {
                PositionLimit = r.PositionLimit,
                VersionInfo = r.VersionInfo,
                ReOrdersMainID = r.ReOrdersMainID,


            });

        return itemsQuery;
    }

}