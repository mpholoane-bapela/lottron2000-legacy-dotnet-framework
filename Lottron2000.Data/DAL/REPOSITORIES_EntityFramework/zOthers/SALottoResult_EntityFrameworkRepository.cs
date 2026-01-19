using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SALottoResult_EntityFrameworkRepository : ISALottoResultRepository
    {
        #region COMMON QUERIES
        public IQueryable<SALottoResult> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResults
                        orderby i.DrawNo descending
                        select i;

            return items;
        }

        public SALottoResult GetByID(string entityID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResults
                        where i.SALottoResultID == entityID
                        select i;

            return items.FirstOrDefault();
        }

        public SALottoResult GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResults
                        where i.DrawNo == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public IQueryable<SALottoResult> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResults
                        where i.DrawDate > fromDate && i.DrawDate < toDate
                        orderby i.DrawNo descending
                        select i;

            return items;
        }

        public IQueryable<SALottoResult> GetByRange(DateTime fromDate, DateTime toDate,int minCheckSumCount,int maxCheckSumCount)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResults
                        where i.DrawDate > fromDate && i.DrawDate < toDate && i.SALottoResultCheckSums.FirstOrDefault().ResultCheckSumSa.Count > minCheckSumCount && i.SALottoResultCheckSums.FirstOrDefault().ResultCheckSumSa.Count < maxCheckSumCount
                        orderby i.DrawNo descending
                        select i;

            return items;
        }

        public void Insert(SALottoResult sALottoResult)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SALottoResults.Add(sALottoResult);
                context.SaveChanges();
            }
        }

        public void Update(SALottoResult sALottoResult)
        {
            using (LottronEntities context = new LottronEntities())
            {
                //context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SALottoResults.Find(sALottoResult.DrawNo);

                if (entityToUpdate != null)
                {
                    entityToUpdate.CheckSum = sALottoResult.CheckSum;
                    entityToUpdate.ItemID = sALottoResult.ItemID;
                    entityToUpdate.SALottoResultID = sALottoResult.SALottoResultID;

                    context.SaveChanges();
                }
            }
        }

        public void Delete(SALottoResult sALottoResult)
        {
            //DeleteByID(sALottoResult.SALottoResultID);
        }

        //public void DeleteByID(string sALottoResultID)
        //{
        //    using (LottronEntities context = new LottronEntities())
        //    {
        //        context.Configuration.AutoDetectChangesEnabled = false;
        //        var entityToUpdate = context.SALottoResults.Find(sALottoResultID);
        //        if (entityToUpdate != null)
        //        {
        //            context.SALottoResults.Remove(entityToUpdate);
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