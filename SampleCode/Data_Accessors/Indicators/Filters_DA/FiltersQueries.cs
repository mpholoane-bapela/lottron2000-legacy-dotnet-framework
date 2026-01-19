using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class RecommendationFilterQueries
    {
        private IGenericDbRepository _genericRepository;

        public RecommendationFilterQueries(IGenericDbRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }


        public async Task<List<RecommendationFilter>> GetAllEntitiesAsync(bool? active = null)
        {
            var items = GetAllEntitiesQuery();

            if (active != null)
            { items = items.Where(a => a.Active == active); }

            return await items.ToListAsync();
        }



        //private IQueryable<RecommendationFilter_ViewDTO> GetAllQuery()
        //{
        //    var itemsQuery = _genericRepository.GetAll<RecommendationFilter>()
        //        .AsNoTracking()
        //        .Select(r => new RecommendationFilter_ViewDTO
        //        {
        //            Active = r.Active,
        //            BaseDealOpenPrice = r.BaseDealOpenPrice,
        //            RecommendationFilterID = r.RecommendationFilterID,
        //            Budget_TotalForRecommendationFilter = r.Budget_TotalForRecommendationFilter,
        //            DealOpen_MinRank_RelToPriceLimit = r.DealOpen_MinRank_RelToPriceLimit,
        //            OrderType_Default = r.OrderType_Default,

        //            DealOpen_OffSetAmount_RelToMarket = r.DealOpen_OffSetAmount_RelToMarket,
        //            DealOpen_OffSetType_RelToMarket = r.DealOpen_OffSetType_RelToMarket,
        //            DealOpen_InstanceGap_ChangeScaleAmount_Default = r.DealOpen_InstanceGap_ChangeScaleAmount_Default,
        //            DealOpen_ChangeScaleType_Default = r.DealOpen_ChangeScaleType_Default,
        //            DealOpen_ChangeScaleRef_Default = r.DealOpen_ChangeScaleRef_Default,

        //        });

        //    return itemsQuery;
        //}


        private IQueryable<RecommendationFilter> GetAllEntitiesQuery()
        {
            var itemsQuery = _genericRepository.GetAll<RecommendationFilter>()
                .AsNoTracking();

            return itemsQuery;
        }

    }
}