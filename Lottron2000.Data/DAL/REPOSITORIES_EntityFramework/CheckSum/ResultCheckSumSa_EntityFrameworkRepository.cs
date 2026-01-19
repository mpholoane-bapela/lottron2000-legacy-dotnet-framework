using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class ResultCheckSumSa_EntityFrameworkRepository : IResultCheckSumSaRepository
    {
        #region COMMON QUERIES
        public IQueryable<ResultCheckSumSa> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.ResultCheckSumSas
                        orderby i.Created descending
                        select i;

            return items;
        }

        public ResultCheckSumSa GetByID(string resultCheckSumSaID)
        {
            var context = new LottronEntities();
            var items = from i in context.ResultCheckSumSas
                        where i.ResultCheckSumSaID == resultCheckSumSaID
                        select i;

            return items.FirstOrDefault();
        }

        public ResultCheckSumSa GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.ResultCheckSumSas
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public ResultCheckSumSa GetByCheckSum(int checkSum)
        {
            var context = new LottronEntities();
            var items = from i in context.ResultCheckSumSas
                        where i.CheckSum == checkSum
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(ResultCheckSumSa resultCheckSumSa)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.ResultCheckSumSas.Add(resultCheckSumSa);
                context.SaveChanges();
            }
        }

        public void Update(ResultCheckSumSa resultCheckSumSa)
        {

        }

        public void Delete(ResultCheckSumSa resultCheckSumSa)
        {
            DeleteByID(resultCheckSumSa.ResultCheckSumSaID);
        }

        public void DeleteByID(string resultCheckSumSaID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.ResultCheckSumSas.Find(resultCheckSumSaID);
                if (entityToUpdate != null)
                {
                    context.ResultCheckSumSas.Remove(entityToUpdate);
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