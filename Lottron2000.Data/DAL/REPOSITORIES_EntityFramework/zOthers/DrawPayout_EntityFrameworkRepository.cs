using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class DrawPayout_EntityFrameworkRepository : IDrawPayoutRepository
    {
        #region COMMON QUERIES
        public IQueryable<DrawPayout> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.DrawPayouts
                        orderby i.ItemID ascending
                        select i;

            return items;
        }


        public DrawPayout GetByID(string drawPayoutID)
        {
            var context = new LottronEntities();
            var items = from i in context.DrawPayouts
                        where i.DrawPayoutID == drawPayoutID
                        select i;

            return items.FirstOrDefault();
        }

        public DrawPayout GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.DrawPayouts
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(DrawPayout drawPayout)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.DrawPayouts.Add(drawPayout);
                context.SaveChanges();
            }
        }

        public void Delete(DrawPayout drawPayout)
        {
            DeleteByID(drawPayout.ItemID);
        }

        public void DeleteByID(int drawPayoutID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.DrawPayouts.Find(drawPayoutID);
                if (entityToUpdate != null)
                {
                    context.DrawPayouts.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(DrawPayout drawPayout)
        { }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}