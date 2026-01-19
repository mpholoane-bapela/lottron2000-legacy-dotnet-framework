using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class WinningNumberPermutation_EntityFrameworkRepository : IWinningNumberPermutationRepository
    {
        #region COMMON QUERIES
        public IQueryable<WinningNumberPermutation> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.WinningNumberPermutations
                        orderby i.ID descending
                        select i;

            return items;
        }

        public WinningNumberPermutation GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.WinningNumberPermutations
                        where i.ID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public IQueryable<WinningNumberPermutation> GetByRange(int minCheckSum, int maxCheckSum)
        {
            var context = new LottronEntities();
            var items = from i in context.WinningNumberPermutations
                        where i.CheckSum > minCheckSum && i.CheckSum < maxCheckSum
                        orderby i.CheckSum descending
                        select i;
            return items;
        }


        //public IQueryable<WinningNumberPermutation> GetByRange(int minCheckSum,int maxCheckSum)
        //{
        //    var dataToReturn;
        //    using (LottronEntities context = new LottronEntities())
        //    {
        //        context.Configuration.AutoDetectChangesEnabled = false;
        //        context.WinningNumberPermutations.Where(a => a.CheckSum > minCheckSum && a.CheckSum < maxCheckSum);
        //    }
        //}

        public int CountGetByRange(int minCheckSum, int maxCheckSum)
        {
            int itemsCount = 0;
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                itemsCount = context.WinningNumberPermutations.Where(a => a.CheckSum > minCheckSum && a.CheckSum < maxCheckSum).Count();
            }
            return itemsCount;
        }



        public void Insert(WinningNumberPermutation winningNumberPermutation)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.WinningNumberPermutations.Add(winningNumberPermutation);
                context.SaveChanges();
            }
        }

        public void Delete(WinningNumberPermutation winningNumberPermutation)
        {
            DeleteByID(winningNumberPermutation.ID);
        }

        public void DeleteByID(int winningNumberPermutationID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.WinningNumberPermutations.Find(winningNumberPermutationID);
                if (entityToUpdate != null)
                {
                    context.WinningNumberPermutations.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(WinningNumberPermutation winningNumberPermutation)
        { }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}