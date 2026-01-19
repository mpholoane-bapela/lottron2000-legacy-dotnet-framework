using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public class PlayingSession_EntityFrameworkRepository : IPlayingSessionRepository
    {
        #region COMMON QUERIES
        public IQueryable<PlayingSession> GetAll()
        {
            var context = new LottronEntities();
            var items = from i in context.PlayingSessions
                        orderby i.Created descending
                        select i;

            return items;
        }

        public PlayingSession GetByID(string playingSessionID)
        {
            var context = new LottronEntities();
            var items = from i in context.PlayingSessions
                        where i.PlayingSessionID == playingSessionID
                        select i;

            return items.FirstOrDefault();
        }

        public PlayingSession GetByItemID(int itemID)
        {
            var context = new LottronEntities();
            var items = from i in context.PlayingSessions
                        where i.ItemID == itemID
                        select i;

            return items.FirstOrDefault();
        }

        public void Update(PlayingSession playingSession)
        {

        }

        public void Insert(PlayingSession playingSession)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.PlayingSessions.Add(playingSession);
                context.SaveChanges();
            }
        }

        public void Delete(PlayingSession playingSession)
        {
            DeleteByID(playingSession.PlayingSessionID);
        }

        public void DeleteByID(string playingSessionID)
        {
            using (LottronEntities context = new LottronEntities())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                var entityToUpdate = context.PlayingSessions.Find(playingSessionID);
                if (entityToUpdate != null)
                {
                    context.PlayingSessions.Remove(entityToUpdate);
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