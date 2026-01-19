using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SimulatedDrawWinningNumber_EntityFrameworkRepository : ISimulatedDrawWinningNumberRepository
    {
        #region COMMON QUERIES
        public IQueryable<SimulatedDrawWinningNumber> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawWinningNumbers
                        orderby i.Created descending
                        select i;

            return items;
        }

        public SimulatedDrawWinningNumber GetByID(string simulatedDrawWinningNumberID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawWinningNumbers
                        where i.SimulatedDrawWinningNumbersID == simulatedDrawWinningNumberID
                        select i;

            return items.FirstOrDefault();
        }

        public SimulatedDrawWinningNumber GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawWinningNumbers
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SimulatedDrawWinningNumber simulatedDrawWinningNumber)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SimulatedDrawWinningNumbers.Add(simulatedDrawWinningNumber);
                context.SaveChanges();
            }
        }

        public void Delete(SimulatedDrawWinningNumber simulatedDrawWinningNumber)
        {
            DeleteByID(simulatedDrawWinningNumber.SimulatedDrawWinningNumbersID);
        }

        public void DeleteByID(string simulatedDrawWinningNumberID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SimulatedDrawWinningNumbers.Find(simulatedDrawWinningNumberID);
                if (entityToUpdate != null)
                {
                    context.SimulatedDrawWinningNumbers.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(SimulatedDrawWinningNumber simulatedDrawWinningNumber)
        { }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}