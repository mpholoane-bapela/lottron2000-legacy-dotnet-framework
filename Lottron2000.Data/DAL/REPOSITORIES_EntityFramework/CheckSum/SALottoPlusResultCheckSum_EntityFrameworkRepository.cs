using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SALottoPlusResultCheckSum_EntityFrameworkRepository : ISALottoPlusResultCheckSumRepository
    {
        #region COMMON QUERIES
        public IQueryable<SALottoPlusResultCheckSum> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResultCheckSums
                        orderby i.ResultCheckSumSa.Count descending
                        select i;

            return items;
        }

        public SALottoPlusResultCheckSum GetByID(string sALottoPlusResultCheckSumID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResultCheckSums
                        where i.SALottoPlusResultCheckSumID == sALottoPlusResultCheckSumID
                        select i;

            return items.FirstOrDefault();
        }

        public SALottoPlusResultCheckSum GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResultCheckSums
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SALottoPlusResultCheckSum sALottoPlusResultCheckSum)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SALottoPlusResultCheckSums.Add(sALottoPlusResultCheckSum);
                context.SaveChanges();
            }
        }

        public void Update(SALottoPlusResultCheckSum sALottoPlusResultCheckSum)
        {

        }

        public void Delete(SALottoPlusResultCheckSum sALottoPlusResultCheckSum)
        {
            DeleteByID(sALottoPlusResultCheckSum.SALottoPlusResultCheckSumID);
        }

        public void DeleteByID(string sALottoPlusResultCheckSumID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SALottoPlusResultCheckSums.Find(sALottoPlusResultCheckSumID);
                if (entityToUpdate != null)
                {
                    context.SALottoPlusResultCheckSums.Remove(entityToUpdate);
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