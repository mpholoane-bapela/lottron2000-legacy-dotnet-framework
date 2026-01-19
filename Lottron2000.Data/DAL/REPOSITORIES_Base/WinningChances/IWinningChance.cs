using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public interface IWinningChanceRepository : IRepository<WinningChance>
    {
        //IQueryable<WinningChance> GetByDivision(string division);
        WinningChance GetByID(string entityID);
    }
}