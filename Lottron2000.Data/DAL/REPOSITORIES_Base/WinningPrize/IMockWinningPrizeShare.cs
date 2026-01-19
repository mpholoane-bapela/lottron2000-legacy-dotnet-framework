using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public interface IMockWinningPrizeShareRepository : IRepository<MockWinningPrizeShare>
    {
        MockWinningPrizeShare GetByID(string mockWinningPrizeShareID);
    }
}