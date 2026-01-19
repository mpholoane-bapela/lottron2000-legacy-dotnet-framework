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
    public static class MockWinningPrizeGenerator
    {
        public static void GenerateWinningPrizes()
        {
            GenerateWinningPrizeDbItems();
        }

        private static void GenerateWinningPrizeDbItems()
        {
            #region LOTTIN WINNINGS
            var saLottoWinnings = SALottoResultBL.GetAll().Where(a => a.Div1 > 0);

            foreach (var item in saLottoWinnings)
            {
                #region PRIZE
                MockWinningPrize mockPrize = new MockWinningPrize();
                mockPrize.CountryID = "ZA";
                mockPrize.Created = DateTime.Now;
                
                mockPrize.Div1 = item.Div1;
                mockPrize.Div2 = item.Div2;
                mockPrize.Div3 = item.Div3;
                mockPrize.Div4 = item.Div4;
                mockPrize.Div5 = item.Div5;
                mockPrize.Div6 = item.Div6;
                mockPrize.Div7 = item.Div7;

                mockPrize.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.MainLotto.ToString();
                mockPrize.MockWinningPrizeID = Guid.NewGuid().ToString();
                mockPrize.SourceDrawNo = item.DrawNo;

                MockWinningPrizeBL.Insert(mockPrize);
                #endregion

                
                #region SHARE
                MockWinningPrizeShare mockShare = new MockWinningPrizeShare();
                mockShare.CountryID = "ZA";
                mockShare.Created = DateTime.Now;
                
                mockShare.Div1Winners = item.Div1Winners;
                mockShare.Div2Winners = item.Div2NoWinners;
                mockShare.Div3Winners = item.Div3Winners;
                mockShare.Div4Winners = item.Div4Winners;
                mockShare.Div5Winners = item.Div5Winners;
                mockShare.Div6Winners = item.Div6NoWinners;
                mockShare.Div7Winners = item.Div7Winners;

                mockShare.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.MainLotto.ToString();
                mockShare.MockWinningPrizeShareID = Guid.NewGuid().ToString();
                mockShare.SourceDrawNo = item.DrawNo;
                MockWinningPrizeShareBL.Insert(mockShare);
                #endregion
            }
            #endregion



            #region LOTTO PLUS
            var saLottoPlusWinnings = SALottoPlusResultBL.GetAll().Where(a => a.Div1 > 0);

            foreach (var item in saLottoPlusWinnings)
            {
                #region PRIZE
                MockWinningPrize mockPrize = new MockWinningPrize();
                mockPrize.CountryID = "ZA";
                mockPrize.Created = DateTime.Now;

                mockPrize.Div1 = item.Div1;
                mockPrize.Div2 = item.Div2;
                mockPrize.Div3 = item.Div3;
                mockPrize.Div4 = item.Div4;
                mockPrize.Div5 = item.Div5;
                mockPrize.Div6 = item.Div6;
                mockPrize.Div7 = item.Div7;

                mockPrize.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus.ToString();
                mockPrize.MockWinningPrizeID = Guid.NewGuid().ToString();
                mockPrize.SourceDrawNo = item.DrawNo;

                MockWinningPrizeBL.Insert(mockPrize);
                #endregion


                #region SHARE
                MockWinningPrizeShare mockShare = new MockWinningPrizeShare();
                mockShare.CountryID = "ZA";
                mockShare.Created = DateTime.Now;

                mockShare.Div1Winners = item.Div1Winners;
                mockShare.Div2Winners = item.Div2NoWinners;
                mockShare.Div3Winners = item.Div3Winners;
                mockShare.Div4Winners = item.Div4Winners;
                mockShare.Div5Winners = item.Div5Winners;
                mockShare.Div6Winners = item.Div6NoWinners;
                mockShare.Div7Winners = item.Div7Winners;

                mockShare.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus.ToString();
                mockShare.MockWinningPrizeShareID = Guid.NewGuid().ToString();
                mockShare.SourceDrawNo = item.DrawNo;
                MockWinningPrizeShareBL.Insert(mockShare);
                #endregion
            }
            
            #endregion

        }
    
    }
}