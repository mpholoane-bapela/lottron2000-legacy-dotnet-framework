using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class SimulatedDrawTicket_EntityFrameworkRepository : ISimulatedDrawTicketRepository
    {
        #region COMMON QUERIES
        public IQueryable<SimulatedDrawTicket> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawTickets
                        orderby i.ID descending
                        select i;

            return items;
        }


        public SimulatedDrawTicket GetByID(string simulatedDrawTicketID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawTickets
                        where i.SimulatedDrawTicketID == simulatedDrawTicketID
                        select i;

            return items.FirstOrDefault();
        }

        public SimulatedDrawTicket GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.SimulatedDrawTickets
                        where i.ID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Insert(SimulatedDrawTicket simulatedDrawTicket)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.SimulatedDrawTickets.Add(simulatedDrawTicket);
                context.SaveChanges();
            }
        }

        public void Delete(SimulatedDrawTicket simulatedDrawTicket)
        {
            DeleteByID(simulatedDrawTicket.SimulatedDrawTicketID);
        }

        public void DeleteByID(string simulatedDrawTicketID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.SimulatedDrawTickets.Find(simulatedDrawTicketID);
                if (entityToUpdate != null)
                {
                    context.SimulatedDrawTickets.Remove(entityToUpdate);
                    context.SaveChanges();
                }
            }
        }

        public void Update(SimulatedDrawTicket simulatedDrawTicket)
        {

        }
        #endregion

        /*
#region EXTRA METHODS
#endregion
*/
    }
}