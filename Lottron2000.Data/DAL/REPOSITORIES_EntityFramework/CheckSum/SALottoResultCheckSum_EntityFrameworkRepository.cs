using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SALottoResultCheckSum_EntityFrameworkRepository : ISALottoResultCheckSumRepository
    {
        #region COMMON QUERIES
        public IQueryable<SALottoResultCheckSum> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResultCheckSums
                        orderby i.ResultCheckSumSa.Count descending
                        select i;

            return items;
        }

        public SALottoResultCheckSum GetByID(string sALottoResultCheckSumID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResultCheckSums
                        where i.SALottoResultCheckSumID == sALottoResultCheckSumID
                        select i;

            return items.FirstOrDefault();
        }

        public SALottoResultCheckSum GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SALottoResultCheckSums
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SALottoResultCheckSum sALottoResultCheckSum)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SALottoResultCheckSums.Add(sALottoResultCheckSum);
                context.SaveChanges();
            }
        }

        public void Update(SALottoResultCheckSum sALottoResultCheckSum)
        {

        }

        public void Delete(SALottoResultCheckSum sALottoResultCheckSum)
        {
            DeleteByID(sALottoResultCheckSum.SALottoResultCheckSumID);
        }

        public void DeleteByID(string sALottoResultCheckSumID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SALottoResultCheckSums.Find(sALottoResultCheckSumID);
                if (entityToUpdate != null)
                {
                    context.SALottoResultCheckSums.Remove(entityToUpdate);
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