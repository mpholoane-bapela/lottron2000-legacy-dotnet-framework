using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SimulatedDrawPrize_EntityFrameworkRepository : ISimulatedDrawPrizeRepository
    {
        #region COMMON QUERIES
        public IQueryable<SimulatedDrawPrize> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawPrizes
                        orderby i.Created descending
                        select i;

            return items;
        }


        public SimulatedDrawPrize GetByID(string simulatedDrawPrizeID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawPrizes
                        where i.SimulatedDrawPrizeID == simulatedDrawPrizeID
                        select i;

            return items.FirstOrDefault();
        }

        public SimulatedDrawPrize GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawPrizes
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SimulatedDrawPrize simulatedDrawPrize)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SimulatedDrawPrizes.Add(simulatedDrawPrize);
                context.SaveChanges();
            }
        }

        public void Delete(SimulatedDrawPrize simulatedDrawPrize)
        {
            DeleteByID(simulatedDrawPrize.SimulatedDrawPrizeID);
        }

        public void Update(SimulatedDrawPrize simulatedDrawPrize)
        {
        }

        public void DeleteByID(string simulatedDrawPrizeID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SimulatedDrawPrizes.Find(simulatedDrawPrizeID);
                if (entityToUpdate != null)
                {
                    context.SimulatedDrawPrizes.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}