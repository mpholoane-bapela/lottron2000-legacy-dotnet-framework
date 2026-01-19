using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceQueries
    {
        private IGenericDbRepository _genericRepository;

        public BotInstanceQueries(IGenericDbRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public List<Instance_DTO> GetAll()
        {
            var items = GetAllQuery();

            return items.ToList();
        }


        public async Task<List<BotInstance>> GetAllEntitiesAsync()
        {
            var items = GetAllEntitiesQuery();


            return await items.ToListAsync();
        }

        public async Task<List<Instance_DTO>> GetAllAsync()
        {
            var items = GetAllQuery();

            return await items.ToListAsync();
        }

        public async Task<Instance_DTO> GetByIDAsync(string botInstanceID)
        {
            var items = GetAllQuery().Where(a => a.BotInstanceID == botInstanceID);

            return await items.FirstOrDefaultAsync();
        }

        public async Task<List<Instance_DTO>> GetForBotAsync(string botID, bool? active = null)
        {
            var items = GetAllQuery().Where(a => a.BotID == botID);

            if (active != null)
            {
                items = items.Where(a => a.Active == active);
            }

            return await items.ToListAsync();
        }

        private IQueryable<Instance_DTO> GetAllQuery()
        {
            //var itemsQuery = _genericRepository.GetAll<BotInstance>().Include(a => a.Product)
            var itemsQuery = _genericRepository.GetAll<BotInstance>()
                .AsNoTracking()
                .Select(r => new Instance_DTO
                {
                    Active = r.Active,
                    BotID = r.BotID,
                    BotInstanceID = r.BotInstanceID,
                    Budget = r.Budget,
                    BudgetAllocationType = r.BudgetAllocationType,
                    BudgetPercentageOfParent = r.BudgetPercentageOfParent,
                    DealOpen_Price_Initial = r.DealOpen_Price_Initial,
                    DealOpen_Price_Final = r.DealOpen_Price_Final,
                    InstanceGap_Reference = r.InstanceGap_Reference,
                    InstanceGap_Percent = r.InstanceGap_Percent,
                    OrderVolume_Organic = r.OrderVolume_Organic,
                    OrderVolume_Calibrated = r.OrderVolume_Calibrated,
                    //OrderVolume_ExpectedTotal = r.OrderVolume_ExpectedTotal,
                    Priority = r.Priority,

                    VersionInfo = r.VersionInfo,

                    DealOpen_FeeAmount = r.DealOpen_FeeAmount,

                });

            return itemsQuery;
        }

        private IQueryable<BotInstance> GetAllEntitiesQuery()
        {
            var itemsQuery = _genericRepository.GetAll<BotInstance>()
               .AsNoTracking();

            return itemsQuery;
        }

    }
}