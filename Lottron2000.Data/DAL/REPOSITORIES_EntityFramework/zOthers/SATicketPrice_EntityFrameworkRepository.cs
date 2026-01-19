using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;
using Lottron2000.Ydin;

namespace Lottron2000.Data
{
    public class SATicketPrice_EntityFrameworkRepository : ISATicketPriceRepository
    {
        #region COMMON QUERIES
        public IQueryable<SATicketPrice> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SATicketPrices
                        orderby i.ID ascending
                        select i;

            return items;
        }


        //public SATicketPrice GetByID(string sATicketPrice)
        //{
        //    var context = new LottronEntities();
        //    var items = from i in context.SATicketPrices
        //                where i.SATicketPriceID == entityID
        //                select i;

        //    return items.FirstOrDefault();
        //}

        public SATicketPrice GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SATicketPrices
                        where i.ID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public SATicketPrice GetByDrawSubCategory(LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
        {
            string drawSubCategoryAsString = drawSubCategory.ToString();
            var context = new LottronEntities();
            var items = from i in context.SATicketPrices
                        where i.LottoType == drawSubCategoryAsString
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SATicketPrice sATicketPrice)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SATicketPrices.Add(sATicketPrice);
                context.SaveChanges();
            }
        }

        public void Delete(SATicketPrice sATicketPrice)
        {
            DeleteByID(sATicketPrice.ID);
        }

        public void DeleteByID(int sATicketPriceID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SATicketPrices.Find(sATicketPriceID);
                if (entityToUpdate != null)
                {
                    context.SATicketPrices.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(SATicketPrice sATicketPrice)
        { }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}