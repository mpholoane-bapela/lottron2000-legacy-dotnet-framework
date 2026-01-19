using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexis.Ydin;
using Lottron2000.Data;
using Lottron2000.Models;

namespace Lottron2000.BusinessLogic
{
    public static class MockWinningPrizeGenerator
    {
        public partial class Payout
        {
            #region VARIBALES ERROR HANDLING
            private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
            private static string ERROR_OCCURED_IN_CLASS = "MockWinningPrizeGenerator.Payout";
            private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
            private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
            #endregion

            private static ILog _logger;
            public static void Init(ILog logger)
            {
                _logger = logger;
            }

            public static List<DrawWinningPrizeSet> GenerateHistoricalRandom(WinningPrizes winningPrizesParms)
            {
                try
                {
                    var allmockPrizes = MockWinningPrizeBL.GetAll().ToList();
                    var allmockPrizeShares = MockWinningPrizeShareBL.GetAll().ToList();

                    RandomNumbers randomNumGenerator = new RandomNumbers();
                    var indexPool = randomNumGenerator.GetUniqueRandomNumbers((winningPrizesParms.SetsQuantity * 2), 1, allmockPrizes.Count);

                    var mainLottoIndexPool = indexPool.Take(winningPrizesParms.SetsQuantity).ToList();
                    var lottoPlusIndexPool = indexPool.Skip(winningPrizesParms.SetsQuantity).Take(winningPrizesParms.SetsQuantity).ToList();

                    List<DrawWinningPrizeSet> drawWinningPrizeSets = new List<DrawWinningPrizeSet>();

                    for (int i = 0; i < winningPrizesParms.SetsQuantity; i++)
                    {
                        DrawWinningPrizeSet prizeSet = new DrawWinningPrizeSet();

                        int mainLottoIndex = mainLottoIndexPool[i];
                        var MainLottoPrizeAndWinner = CreateWinningPrizeAndWinner(allmockPrizes, allmockPrizeShares, mainLottoIndex);
                        prizeSet.MainLottoPrizeAndWinner = MainLottoPrizeAndWinner;

                        int lottoPlusIndex = lottoPlusIndexPool[i];
                        var lottoPlusPrizeAndWinner = CreateWinningPrizeAndWinner(allmockPrizes, allmockPrizeShares, lottoPlusIndex);
                        prizeSet.LottoPlusPrizeAndWinner = lottoPlusPrizeAndWinner;

                        drawWinningPrizeSets.Add(prizeSet);
                    }

                    return drawWinningPrizeSets;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomNumberSets";
                    string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            private static WinningPrizeAndWinner CreateWinningPrizeAndWinner(List<MockWinningPrize> mockWinningPrizes, List<MockWinningPrizeShare> mockWinningShare, int index)
            {
                try
                {
                    WinningPrizeAndWinner winningPrizeAndWinner = new WinningPrizeAndWinner();

                    WinningPrize winningPrize = MockWinningPrizeBL.MapToWinningPrize(mockWinningPrizes[index]);
                    winningPrizeAndWinner.WinningPrize = winningPrize;


                    WinningPrizeWinner winningPrizeWinner = MockWinningPrizeShareBL.MapToWinningPrizeShare(mockWinningShare[index]);
                    winningPrizeAndWinner.WinningPrizeWinner = winningPrizeWinner;
                    return winningPrizeAndWinner;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "CreateWinningPrizeAndWinner";
                    string errorMethodSignature = "private static WinningPrizeAndWinner CreateWinningPrizeAndWinner(List<MockWinningPrize> mockWinningPrizes, List<MockWinningPrizeShare> mockWinningShare,int index)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }
        }
    }
}