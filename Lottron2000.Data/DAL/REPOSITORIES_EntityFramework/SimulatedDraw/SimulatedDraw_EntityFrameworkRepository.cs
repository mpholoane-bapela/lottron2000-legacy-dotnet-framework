using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SimulatedDraw_EntityFrameworkRepository : ISimulatedDrawRepository
    {
        #region COMMON QUERIES
        public IQueryable<SimulatedDraw> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDraws
                        orderby i.Created descending
                        select i;

            return items;
        }

        public SimulatedDraw GetByID(string simulatedDrawID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDraws
                        where i.SimulatedDrawID == simulatedDrawID
                        select i;

            return items.FirstOrDefault();
        }

        public SimulatedDraw GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDraws
                        where i.ID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SimulatedDraw simulatedDraw)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SimulatedDraws.Add(simulatedDraw);
                context.SaveChanges();
            }
        }

        public void Delete(SimulatedDraw simulatedDraw)
        {
            DeleteByID(simulatedDraw.SimulatedDrawID);
        }

        public void DeleteByID(string simulatedDrawID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SimulatedDraws.Find(simulatedDrawID);
                if (entityToUpdate != null)
                {
                    context.SimulatedDraws.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(SimulatedDraw simulatedDraw)
        {
        }

        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}