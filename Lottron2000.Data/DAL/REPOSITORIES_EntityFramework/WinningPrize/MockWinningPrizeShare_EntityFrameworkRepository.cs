using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class MockWinningPrizeShare_EntityFrameworkRepository : IMockWinningPrizeShareRepository
    {
        #region COMMON QUERIES
        public IQueryable<MockWinningPrizeShare> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.MockWinningPrizeShares
                        orderby i.Created descending
                        select i;

            return items;
        }


        public MockWinningPrizeShare GetByID(string mockWinningPrizeShareID)
        {
            var context = new LottronEntities();
            var items = from i in context.MockWinningPrizeShares
                        where i.MockWinningPrizeShareID == mockWinningPrizeShareID
                        select i;

            return items.FirstOrDefault();
        }

        public MockWinningPrizeShare GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.MockWinningPrizeShares
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(MockWinningPrizeShare mockWinningPrizeShare)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.MockWinningPrizeShares.Add(mockWinningPrizeShare);
                context.SaveChanges();
            }
        }

        public void Delete(MockWinningPrizeShare mockWinningPrizeShare)
        {
            DeleteByID(mockWinningPrizeShare.MockWinningPrizeShareID);
        }

        public void Update(MockWinningPrizeShare mockWinningPrizeShare)
        {
        }

        public void DeleteByID(string mockWinningPrizeShareID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.MockWinningPrizeShares.Find(mockWinningPrizeShareID);
                if (entityToUpdate != null)
                {
                    context.MockWinningPrizeShares.Remove(entityToUpdate);
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