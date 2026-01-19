using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.BusinessLogic;
using Lottron2000.Data;
using Lottron2000.Ydin;

namespace Lottron2000.DataExtraction.ConsoleApps
{
    class Program
    {
        static void Main(string[] args)
        {
            InitilizeStuff();

            string fileLocation = "";


            //CheckSumCounts.CreateCheckSums();
            
            //MockWinningPrizeGenerator.GenerateWinningPrizes();
            
            GuidGenerator.GenerateAndSaveToTextFile(1, fileLocation);

            //SALottoPlusResultExtract.AddFieldValues();
            //SALottoResultExtract.AddFieldValues();
        }

        private static void InitilizeStuff()
        {
            var Ilogger = DependecyStuffInitializor.ILogger.GetDefault();

            WinningNumberPermutationBL.Init(new WinningNumberPermutation_EntityFrameworkRepository(), Ilogger);
            SATicketPriceBL.Init(new SATicketPrice_EntityFrameworkRepository(), Ilogger);
            SALottoResultBL.Init(new SALottoResult_EntityFrameworkRepository(), Ilogger);
            SALottoPlusResultBL.Init(new SALottoPlusResult_EntityFrameworkRepository(), Ilogger);

            ResultCheckSumSaBL.Init(new ResultCheckSumSa_EntityFrameworkRepository(), Ilogger);
            SALottoPlusResultCheckSumBL.Init(new SALottoPlusResultCheckSum_EntityFrameworkRepository(), Ilogger);
            SALottoResultCheckSumBL.Init(new SALottoResultCheckSum_EntityFrameworkRepository(), null);

            MockWinningPrizeBL.Init(new MockWinningPrize_EntityFrameworkRepository(), null);
            MockWinningPrizeShareBL.Init(new MockWinningPrizeShare_EntityFrameworkRepository(), null);
        
        }
    }
}