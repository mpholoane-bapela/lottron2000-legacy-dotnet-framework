using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SimulatedDrawTicketResult_EntityFrameworkRepository : ISimulatedDrawTicketResultRepository
    {
        #region COMMON QUERIES
        public IQueryable<SimulatedDrawTicketResult> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawTicketResults
                        orderby i.Created descending
                        select i;

            return items;
        }


        public SimulatedDrawTicketResult GetByID(string simulatedDrawTicketResultID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawTicketResults
                        where i.SimulatedDrawTicketResultID == simulatedDrawTicketResultID
                        select i;

            return items.FirstOrDefault();
        }

        public SimulatedDrawTicketResult GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawTicketResults
                        where i.ID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SimulatedDrawTicketResult simulatedDrawTicketResult)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SimulatedDrawTicketResults.Add(simulatedDrawTicketResult);
                context.SaveChanges();
            }
        }

        public void Delete(SimulatedDrawTicketResult simulatedDrawTicketResult)
        {
            DeleteByID(simulatedDrawTicketResult.SimulatedDrawTicketResultID);
        }

        public void DeleteByID(string simulatedDrawTicketResultID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SimulatedDrawTicketResults.Find(simulatedDrawTicketResultID);
                if (entityToUpdate != null)
                {
                    context.SimulatedDrawTicketResults.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(SimulatedDrawTicketResult simulatedDrawTicketResult)
        { }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}