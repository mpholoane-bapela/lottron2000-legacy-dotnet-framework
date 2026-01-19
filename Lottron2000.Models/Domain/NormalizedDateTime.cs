using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Models
{
    public class NormilizedDateRange
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public NormilizedDateRange(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}