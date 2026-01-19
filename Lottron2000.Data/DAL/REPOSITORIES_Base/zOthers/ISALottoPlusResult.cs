using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public interface ISALottoPlusResultRepository : IRepository<SALottoPlusResult>
    {
        IQueryable<SALottoPlusResult> GetByDateRange(DateTime fromDate, DateTime toDate);
        IQueryable<SALottoPlusResult> GetByRange(DateTime fromDate, DateTime toDate, int minCheckSum, int maxCheckSum);
        SALottoPlusResult GetByID(string entityID);
    }
}