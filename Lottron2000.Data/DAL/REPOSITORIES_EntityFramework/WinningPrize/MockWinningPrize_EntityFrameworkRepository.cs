using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class MockWinningPrize_EntityFrameworkRepository : IMockWinningPrizeRepository
    {
        #region COMMON QUERIES
        public IQueryable<MockWinningPrize> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.MockWinningPrizes
                        orderby i.Created descending
                        select i;

            return items;
        }


        public MockWinningPrize GetByID(string mockWinningPrizeID)
        {
            var context = new LottronEntities();
            var items = from i in context.MockWinningPrizes
                        where i.MockWinningPrizeID == mockWinningPrizeID
                        select i;

            return items.FirstOrDefault();
        }

        public MockWinningPrize GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.MockWinningPrizes
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(MockWinningPrize mockWinningPrize)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.MockWinningPrizes.Add(mockWinningPrize);
                context.SaveChanges();
            }
        }

        public void Delete(MockWinningPrize mockWinningPrize)
        {
            DeleteByID(mockWinningPrize.MockWinningPrizeID);
        }

        public void Update(MockWinningPrize mockWinningPrize)
        {
        }

        public void DeleteByID(string mockWinningPrizeID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.MockWinningPrizes.Find(mockWinningPrizeID);
                if (entityToUpdate != null)
                {
                    context.MockWinningPrizes.Remove(entityToUpdate);
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