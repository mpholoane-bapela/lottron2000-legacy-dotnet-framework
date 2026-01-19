using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SALottoPlusResult_EntityFrameworkRepository : ISALottoPlusResultRepository
    {
        #region COMMON QUERIES
        public IQueryable<SALottoPlusResult> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResults
                        orderby i.DrawDate descending
                        select i;

            return items;
        }

        public SALottoPlusResult GetByID(string entityID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResults
                        where i.SALottoPlusResultID == entityID
                        select i;

            return items.FirstOrDefault();
        }

        public SALottoPlusResult GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResults
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public IQueryable<SALottoPlusResult> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResults
                        where i.DrawDate > fromDate && i.DrawDate < toDate
                        orderby i.DrawNo descending
                        select i;

            return items;
        }

        public IQueryable<SALottoPlusResult> GetByRange(DateTime fromDate, DateTime toDate, int minCheckSumCount, int maxCheckSumCount)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoPlusResults
                        where i.DrawDate > fromDate && i.DrawDate < toDate && i.SALottoPlusResultCheckSums.FirstOrDefault().ResultCheckSumSa.Count >= minCheckSumCount && i.SALottoPlusResultCheckSums.FirstOrDefault().ResultCheckSumSa.Count <= maxCheckSumCount
                        orderby i.DrawNo descending
                        select i;

            return items;
        }

        public void Insert(SALottoPlusResult sALottoPlusResult)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SALottoPlusResults.Add(sALottoPlusResult);
                context.SaveChanges();
            }
        }

        public void Update(SALottoPlusResult sALottoPlusResult)
        {
            using (LottronEntities context = new LottronEntities())
            {
                //context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SALottoPlusResults.Find(sALottoPlusResult.DrawNo); //Where(i => i.EnquiryID == theEnquiry.EnquiryID).FirstOrDefault();

                if (entityToUpdate != null)
                {
                    entityToUpdate.CheckSum = sALottoPlusResult.CheckSum;
                    entityToUpdate.ItemID = sALottoPlusResult.ItemID;
                    entityToUpdate.SALottoPlusResultID = sALottoPlusResult.SALottoPlusResultID;

                    //context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(SALottoPlusResult sALottoPlusResult)
        {
            //DeleteByID(sALottoPlusResult.DrawNo);
        }


        //public void DeleteByID(string sALottoPlusResultID)
        //{
        //    using (LottronEntities context = new LottronEntities())
        //    {
        //        context.Configuration.AutoDetectChangesEnabled = false;
        //        var entityToUpdate = context.SALottoPlusResults.Find(sALottoPlusResultID);
        //        if (entityToUpdate != null)
        //        {
        //            context.SALottoPlusResults.Remove(entityToUpdate);
        //            context.SaveChanges();
        //        }
        //    }
        //}

        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}