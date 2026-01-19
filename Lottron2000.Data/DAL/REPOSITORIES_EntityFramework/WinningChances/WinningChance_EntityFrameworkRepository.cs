using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class WinningChance_EntityFrameworkRepository : IWinningChanceRepository
    {
        #region COMMON QUERIES
        public IQueryable<WinningChance> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.WinningChances
                        orderby i.PayoutOrder ascending
                        select i;

            return items;
        }

        public WinningChance GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.WinningChances
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public WinningChance GetByID(string entityID)
        {
            var context = new LottronEntities();
            var items = from i in context.WinningChances
                        where i.WinningChanceID == entityID
                        select i;

            return items.FirstOrDefault();
        }

        public WinningChance GetByDivision(string division)
        {
            var context = new LottronEntities();
            var items = from i in context.WinningChances
                        where i.Division == division
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(WinningChance winningChance)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.WinningChances.Add(winningChance);
                context.SaveChanges();
            }
        }

        public void Delete(WinningChance winningChance)
        {
            DeleteByID(winningChance.ItemID);
        }

        public void DeleteByID(int winningChanceID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.WinningChances.Find(winningChanceID);
                if (entityToUpdate != null)
                {
                    context.WinningChances.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(WinningChance winningChance)
        {
            using (LottronEntities context = new LottronEntities())
            {
                //context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.WinningChances.Find(winningChance.ItemID);

                if (entityToUpdate != null)
                {
                    entityToUpdate.Fraction = winningChance.Fraction;
                    entityToUpdate.Percentage = winningChance.Percentage;

                    //context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
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