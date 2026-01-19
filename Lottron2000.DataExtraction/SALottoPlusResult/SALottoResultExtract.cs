using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottron2000.BusinessLogic;
using Lottron2000.Data;
using Lottron2000.Models;
using Lottron2000.Ydin;

namespace Lottron2000.DataExtraction
{
    public static class SALottoResultExtract
    {
    // Add non default field values for ItemID, entityID and CheckSum
        public static void AddFieldValues()
        {
            var allItems = SALottoResultBL.GetAll().OrderBy(a => a.DrawNo);

            int index = 1;
            foreach (var item in allItems)
            {
                item.ItemID = index;
                item.SALottoResultID = Guid.NewGuid().ToString();
                item.CheckSum = item.Ball1 + item.Ball2 + item.Ball3 + item.Ball4 + item.Ball5 + item.Ball6;

                SALottoResultBL.Update(item);
                index++;
            }
        }
    
    }
}