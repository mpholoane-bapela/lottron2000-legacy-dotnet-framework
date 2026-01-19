using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lottron2000.BusinessLogic;
using Lottron2000.Data;
using Lottron2000.BusinessLogic.BAL.AutoMappers;
using Lottron2000.Models;

namespace Lottron2000.UnitTests
{
    [TestClass]
    public class WinningPrizeGeneratorTest
    {
        [TestMethod]
        public void WinningPrizesTest()
        {
            InitializeCode();

            WinningPrizes winningPrizesParams = new WinningPrizes(1, Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom, Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom);
            var generated1Set = MockWinningPrizeGenerator.Payout.GenerateHistoricalRandom(winningPrizesParams);


            WinningPrizes winningPrizesParams2 = new WinningPrizes(5, Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom, Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom);
            var generatedSet2 = MockWinningPrizeGenerator.Payout.GenerateHistoricalRandom(winningPrizesParams2);

            int x = 0;
        }

        private void InitializeCode()
        {
            WinningNumberPermutationBL.Init(new WinningNumberPermutation_EntityFrameworkRepository(), null);
            SATicketPriceBL.Init(new SATicketPrice_EntityFrameworkRepository(), null);
            SALottoResultBL.Init(new SALottoResult_EntityFrameworkRepository(), null);
            SALottoPlusResultBL.Init(new SALottoPlusResult_EntityFrameworkRepository(), null);

            MockWinningPrizeBL.Init(new MockWinningPrize_EntityFrameworkRepository(), null);
            MockWinningPrizeShareBL.Init(new MockWinningPrizeShare_EntityFrameworkRepository(), null);
        }
    }
}