using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess;

public class CompletedDealQueries
{
    private IGenericDbRepository _genericRepository;

    public CompletedDealQueries(IGenericDbRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<List<CompletedDeal>> GetAllEntitiesAsync()
    {
        var items = GetAllEntitiesQuery();

        return await items.ToListAsync();
    }

    public async Task<CompletedDeal> GetByIDAsync(string BotCompletedDealID)
    {
        var items = GetAllEntitiesQuery().Where(a => a.CompletedDealID == BotCompletedDealID);

        return await items.FirstOrDefaultAsync();
    }

    public CompletedDeal GetByID(string BotCompletedDealID)
    {
        var items = GetAllEntitiesQuery().Where(a => a.CompletedDealID == BotCompletedDealID);

        return items.FirstOrDefault();
    }



    public async Task<List<CompletedDeal>> GetCompletedDeals(string botID, DateTime dateTimeStarted, DateTime dateTimeCompleted)
    {
        var items = GetAllEntitiesQuery().Where(a => a.BotID == botID && a.DateTime_Started >= dateTimeStarted && a.DateTime_Completed < dateTimeCompleted);

        return await items.ToListAsync();
    }

    public async Task<List<CompletedDeal>> GetCompletedDeals(DateTime fromDateTime)
    {
        var items = GetAllEntitiesQuery().Where(a => a.DateTime_Completed >= fromDateTime);

        return await items.ToListAsync();
    }


    public async Task<CompletedDeal> GetByDealID(string dealID)
    {
        var items = GetAllEntitiesQuery().Where(a => a.DealID == dealID);

        return await items.FirstOrDefaultAsync();
    }

    private IQueryable<CompletedDeal> GetAllEntitiesQuery()
    {
        var itemsQuery = _genericRepository.GetAll<CompletedDeal>()
            .AsNoTracking();

        return itemsQuery;
    }

}