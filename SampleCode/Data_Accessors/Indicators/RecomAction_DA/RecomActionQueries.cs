using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class RecommendationActionQueries
    {
        private IGenericDbRepository _genericRepository;

        public RecommendationActionQueries(IGenericDbRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<RecommendationAction>> GetAllEntitiesAsync(string userPortfolioID = null, bool? active = null, string tradeExchange = null)
        {
            var items = GetAllEntitiesQuery();
            return await items.ToListAsync();
        }


        private IQueryable<RecommendationAction> GetAllEntitiesQuery()
        {
            var itemsQuery = _genericRepository.GetAll<RecommendationAction>()
                .AsNoTracking();

            return itemsQuery;
        }

    }
}