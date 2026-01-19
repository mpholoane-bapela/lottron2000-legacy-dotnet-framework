using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SimulatedDrawResult_EntityFrameworkRepository : ISimulatedDrawResultRepository
    {
        #region COMMON QUERIES
        public IQueryable<SimulatedDrawResult> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawResults
                        orderby i.Created descending
                        select i;

            return items;
        }


        //public SimulatedDrawResult GetByID(string simulatedDrawResult)
        //{
        //    var context = new LottronEntities();
        //    var items = from i in context.SimulatedDrawResults
        //                where i.SimulatedDrawResultID == entityID
        //                select i;

        //    return items.FirstOrDefault();
        //}

        public SimulatedDrawResult GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawResults
                        where i.ID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SimulatedDrawResult simulatedDrawResult)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SimulatedDrawResults.Add(simulatedDrawResult);
                context.SaveChanges();
            }
        }

        public void Delete(SimulatedDrawResult simulatedDrawResult)
        {
            DeleteByID(simulatedDrawResult.SimulatedDrawResultID);
        }

        public void DeleteByID(string simulatedDrawResultID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SimulatedDrawResults.Find(simulatedDrawResultID);
                if (entityToUpdate != null)
                {
                    context.SimulatedDrawResults.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(SimulatedDrawResult simulatedDrawResult)
        {
        
        }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}