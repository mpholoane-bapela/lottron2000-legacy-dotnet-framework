using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public interface IResultCheckSumSaRepository : IRepository<ResultCheckSumSa>
    {
        ResultCheckSumSa GetByID(string resultCheckSumSaID);
        ResultCheckSumSa GetByCheckSum(int checkSum);
    }
}