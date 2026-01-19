using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public interface ISALottoResultRepository : IRepository<SALottoResult>
    {
        IQueryable<SALottoResult> GetByDateRange(DateTime fromDate, DateTime toDate);
        IQueryable<SALottoResult> GetByRange(DateTime fromDate, DateTime toDate,int minCheckSum,int maxCheckSum);
        SALottoResult GetByID(string entityID);
    }
}