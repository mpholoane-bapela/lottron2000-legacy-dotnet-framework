using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using TradingBots.Native.Domain;

namespace TradingBots.Native.Infra.DbAccess
{
    public class BotInstanceStateQueries
    {
        private readonly IGenericDbRepository _genericRepository;

        public BotInstanceStateQueries(IGenericDbRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public List<InstanceState_DTO> GetAll(bool? active = null)
        {
            var items = GetAllQuery();

            if (active != null)
            { items = items.Where(a => a.Active == active); }

            return items.ToList();
        }

        public async Task<List<InstanceState_DTO>> GetAllAsync(bool? active = null)
        {
            var items = GetAllQuery();

            if (active != null)
            { items = items.Where(a => a.Active == active); }

            return await items.ToListAsync();
        }


        public async Task<InstanceState_DTO> GetByIDAsync(string botInstanceStateID)
        {
            var items = await GetAllQuery().Where(a => a.BotInstanceStateID == botInstanceStateID)
                .FirstOrDefaultAsync();
            return items;
        }

        public async Task<BotInstanceState> GetEntityByIDAsync(string botInstanceStateID)
        {
            var items = await GetAllEntitiesQuery().Where(a => a.BotInstanceStateID == botInstanceStateID)
                .FirstOrDefaultAsync();
            return items;
        }

        public async Task<InstanceState_DTO> GetByBotInstanceIDAsync(string botInstanceID)
        {
            var items = GetAllQuery().Where(a => a.BotInstanceID == botInstanceID);

            return await items.FirstOrDefaultAsync();
        }

        public async Task<List<InstanceState_DTO>> GetByBotIDAsync(string botID, bool? active = null)
        {
            var items = GetAllQuery()
                .Where(a => a.BotID == botID);

            if (active != null)
            { items = items.Where(a => a.Active == active); }

            return await items.ToListAsync();
        }

        private IQueryable<InstanceState_DTO> GetAllQuery()
        {
            //var itemsQuery = _genericRepository.GetAll<BotInstanceState>().Include(a => a.Product)
            var itemsQuery = _genericRepository.GetAll<BotInstanceState>().Include(a => a.BotInstance)
                .AsNoTracking()
                .Select(r => new InstanceState_DTO
                {
                    Active = r.Active,
                    ApiOrderID_Current = r.ApiOrderID_Current,
                    ApiOrderID_DealOpen = r.ApiOrderID_DealOpen,
                    ApiOrderID_DealClose = r.ApiOrderID_DealClose,
                    ApiTimeStamp_Current = r.ApiTimeStamp_Current,
                    ApiTimeStamp_DealOpen = r.ApiTimeStamp_DealOpen,
                    ApiTimeStamp_DealClose = r.ApiTimeStamp_DealClose,
                    DealID = r.DealID,
                    BotInstanceID = r.BotInstanceID,
                    BotID = r.BotInstance.BotID,
                    BotInstanceStateID = r.BotInstanceStateID,

                    CurrentState = r.CurrentState,
                    PrevState = r.PrevState,
                    TradeWorkflowStatus = r.TradeWorkflowStatus,
                    VersionInfo = r.VersionInfo
                });

            return itemsQuery;
        }

        private IQueryable<BotInstanceState> GetAllEntitiesQuery()
        {
            //var itemsQuery = _genericRepository.GetAll<BotInstanceState>().Include(a => a.Product)
            var itemsQuery = _genericRepository.GetAll<BotInstanceState>().Include(a => a.BotInstance)
                .AsNoTracking();

            return itemsQuery;
        }
    }

}