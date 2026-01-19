using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lottron2000.BusinessLogic;
using Lottron2000.Data;
using Lottron2000.BusinessLogic.BAL.AutoMappers;
using Lottron2000.Models;
using Lottron2000.Ydin;
using System.Collections.Generic;
using System.Collections;

namespace Lottron2000.UnitTests
{
    [TestClass]
    public class LotteryNumbersGeneratorsTests
    {
        
        [TestMethod]
        public void PlayingTicketsTest()
        {
            InitializeCode();
            
            //var numbers = LotteryNumbersGenerator.PlayingTicketNumbers.GenerateRandomNumberSets(1000);
            //int x = 0;

            //var numbers2 = LotteryNumbersGenerator.PlayingTicketNumbers.GenerateRandomNumberSets(5000, Ydin.LottronConstants.PlayingSession.DrawSubCategory.LottoPlus);
            //var numbersCreated = numbers2.Count;
            //int xxx = 0;

            //2000-03-11 00:00:00.000
            DateTime fromDate = new DateTime(2000, 03, 11);
            DateTime toDate = new DateTime(2013, 03, 12);
            //var numbers2 = LotteryNumbersGenerator.PlayingTickets.GetRandomHistoricalWinningNumberSets(10, Ydin.LottronConstants.PlayingSession.DrawSubCategory.LottoPlus,15,22 ,fromDate,toDate);

            //TestLotterNumbersGenerator();//REMOVE JUST NOW

            TestPlayingTheLottery();

            int x = 0;
        }

        private void TestPlayingTheLottery()
        {
            decimal budget = 5000;
            PlayingTickets playingTicketsParams = new PlayingTickets(0, budget, Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.ParameterizedRandom);
            playingTicketsParams.MinCheckSum = 139;
            playingTicketsParams.MaxCheckSum = 141;


            WinningNumbers winningNumbersParams = new WinningNumbers(Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.UserProvided);
            winningNumbersParams.MinCheckSum = 147; winningNumbersParams.MaxCheckSum = 147;


            #region user provided winning numbers
            DrawWinningNumbersCollection userProvidedWinningNumbersCollection = new DrawWinningNumbersCollection();
            LotteryNumbers mainLottoWinningNumbers = new LotteryNumbers(Guid.NewGuid().ToString(),22,36,38,39,40,48,20);
            LotteryNumbers lottoPlusWinningNumbers = new LotteryNumbers(Guid.NewGuid().ToString(), 6, 8, 17, 28, 37, 49, 27);

            List<LotteryNumbers> mainLottoWinningNumbersCol = new List<LotteryNumbers>(){mainLottoWinningNumbers};
            List<LotteryNumbers> lottoPlusWinningNumbersCol = new List<LotteryNumbers>(){lottoPlusWinningNumbers};

            userProvidedWinningNumbersCollection.MainLottoNumbersCollection = mainLottoWinningNumbersCol;
            userProvidedWinningNumbersCollection.LottoPlusNumbersCollection = lottoPlusWinningNumbersCol;
            #endregion


            var lottoPlus = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus;
            WinningPrizes winningPrizes = new WinningPrizes(1, LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom, LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom);

            SimulatedDrawParameters parameters = new SimulatedDrawParameters(playingTicketsParams, winningNumbersParams, winningPrizes, lottoPlus);
            SimulatedDrawDriver.PlayTheLottery("9b416415-dadd-4f0d-bd05-c2976e09df55", parameters, null, userProvidedWinningNumbersCollection);
        }

        private void TestLotterNumbersGenerator()
        {
            PlayingTickets playingTicketsParams = new PlayingTickets(0, 1000, Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.ParameterizedRandom);

            WinningNumbers winningNumbersParams = new WinningNumbers(Ydin.LottronConstants.PlayingSession.NumbersGenerationMethod.ParameterizedRandom);
            winningNumbersParams.MinCheckSum = 160; winningNumbersParams.MaxCheckSum = 200;

            var lottoPlus = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus;
            WinningPrizes winningPrizes = new WinningPrizes(1, LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom, LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom);

            SimulatedDrawParameters parameters = new SimulatedDrawParameters(playingTicketsParams, winningNumbersParams, winningPrizes, lottoPlus);


            LotteryNumbersGenerator.WinningNumbers.GenerateParmatizedRandonNumberSet(parameters);
        }

        private void InitializeCode()
        {
            DrawPayoutBL.Init(new DrawPayout_EntityFrameworkRepository(), null);
            MockWinningPrizeBL.Init(new MockWinningPrize_EntityFrameworkRepository(), null);
            MockWinningPrizeShareBL.Init(new MockWinningPrizeShare_EntityFrameworkRepository(), null);
            PlayingSessionBL.Init(new PlayingSession_EntityFrameworkRepository(), null);
            ResultCheckSumSaBL.Init(new ResultCheckSumSa_EntityFrameworkRepository(), null);
            SALottoPlusResultBL.Init(new SALottoPlusResult_EntityFrameworkRepository(), null);
            SALottoPlusResultCheckSumBL.Init(new SALottoPlusResultCheckSum_EntityFrameworkRepository(), null);
            SALottoResultBL.Init(new SALottoResult_EntityFrameworkRepository(), null);
            SALottoResultCheckSumBL.Init(new SALottoResultCheckSum_EntityFrameworkRepository(), null);
            SATicketPriceBL.Init(new SATicketPrice_EntityFrameworkRepository(), null);
            SimulatedDrawBL.Init(new SimulatedDraw_EntityFrameworkRepository(), null);
            SimulatedDrawPrizeBL.Init(new SimulatedDrawPrize_EntityFrameworkRepository(), null);
            SimulatedDrawResultBL.Init(new SimulatedDrawResult_EntityFrameworkRepository(), null);
            SimulatedDrawTicketBL.Init(new SimulatedDrawTicket_EntityFrameworkRepository(), null);
            SimulatedDrawTicketResultBL.Init(new SimulatedDrawTicketResult_EntityFrameworkRepository(), null);
            SimulatedDrawWinningNumberBL.Init(new SimulatedDrawWinningNumber_EntityFrameworkRepository(),null);
            WinningChanceBL.Init(new WinningChance_EntityFrameworkRepository(), null);
            WinningNumberPermutationBL.Init(new WinningNumberPermutation_EntityFrameworkRepository(), null);
        }
    }
}