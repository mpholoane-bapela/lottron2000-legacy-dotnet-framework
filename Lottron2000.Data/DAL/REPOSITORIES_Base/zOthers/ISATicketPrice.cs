using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;
using Lottron2000.Ydin;

namespace Lottron2000.Data
{
    public interface ISATicketPriceRepository : IRepository<SATicketPrice>
    {
        SATicketPrice GetByDrawSubCategory(LottronConstants.PlayingSession.DrawSubCategory drawSubCategory);
    }
}