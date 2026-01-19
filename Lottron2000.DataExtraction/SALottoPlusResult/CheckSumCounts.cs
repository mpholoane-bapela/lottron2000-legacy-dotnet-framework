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
    public static class CheckSumCounts
    {
        public static void CreateCheckSums()
        {
            //CreateCheckSumsForMainLotto();
            //CreateCheckSumsForLottoPlus();
        }
        
        private static void CreateCheckSumsForMainLotto()
        {
            var dbItems = SALottoResultBL.GetAll().OrderBy(a => a.DrawNo).ToList();
            var allItems = dbItems.GroupBy(a => a.CheckSum).Select(g => new { Item = g.Key, Count = g.Count() });

            int dbItemsCount = dbItems.Count;

            foreach (var item in allItems)
            {
                ResultCheckSumSa dbItem = new ResultCheckSumSa();
                dbItem.CheckSum = (int)item.Item;
                dbItem.Count = item.Count;
                dbItem.Created = DateTime.Now;
                dbItem.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.MainLotto.ToString();
                dbItem.EndDate = DateTime.Now;
                dbItem.LastComputed = DateTime.Now;
                dbItem.OccuranceRatio = item.Count / dbItemsCount;
                dbItem.ResultCheckSumSaID = Guid.NewGuid().ToString();
                dbItem.StartDate = DateTime.Now;

                ResultCheckSumSaBL.Insert(dbItem);
            }

            //Insert the mapping table
            foreach (var dbItem in dbItems)
            {
                var checkSumdbItem = ResultCheckSumSaBL.GetByCheckSum((int)dbItem.CheckSum);

                SALottoResultCheckSum newDbItem = new SALottoResultCheckSum();
                newDbItem.ResultCheckSumSaID = checkSumdbItem.ResultCheckSumSaID;
                newDbItem.SALottoResultCheckSumID = Guid.NewGuid().ToString();
                newDbItem.SALottoResultID = dbItem.SALottoResultID;

                SALottoResultCheckSumBL.Insert(newDbItem);
            }

            int x = 0;
        }

        private static void CreateCheckSumsForLottoPlus()
        {
            var dbItems = SALottoPlusResultBL.GetAll().OrderBy(a => a.DrawNo).ToList();
            var allItems = dbItems.GroupBy(a => a.CheckSum).Select(g => new { Item = g.Key, Count = g.Count() });

            int dbItemsCount = dbItems.Count;

            foreach (var item in allItems)
            {
                ResultCheckSumSa dbItem = new ResultCheckSumSa();
                dbItem.CheckSum = (int)item.Item;
                dbItem.Count = item.Count;
                dbItem.Created = DateTime.Now;
                dbItem.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus.ToString();
                dbItem.EndDate = DateTime.Now;
                dbItem.LastComputed = DateTime.Now;
                dbItem.OccuranceRatio = item.Count / dbItemsCount;
                dbItem.ResultCheckSumSaID = Guid.NewGuid().ToString();
                dbItem.StartDate = DateTime.Now;

                ResultCheckSumSaBL.Insert(dbItem);
            }

            //Insert the mapping table
            foreach (var dbItem in dbItems)
            {
                var checkSumdbItem = ResultCheckSumSaBL.GetByCheckSum((int)dbItem.CheckSum);

                SALottoPlusResultCheckSum newDbItem = new SALottoPlusResultCheckSum();
                newDbItem.ResultCheckSumSaID = checkSumdbItem.ResultCheckSumSaID;
                newDbItem.SALottoPlusResultCheckSumID = Guid.NewGuid().ToString();
                newDbItem.SALottoPlusResultID = dbItem.SALottoPlusResultID;

                SALottoPlusResultCheckSumBL.Insert(newDbItem);
            }
        }
    
    }
}