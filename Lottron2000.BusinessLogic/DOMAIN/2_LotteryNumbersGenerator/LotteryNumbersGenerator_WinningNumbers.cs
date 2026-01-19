using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottron2000.Models;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Ydin;
using Lottron2000.Ydin.Utilities;

namespace Lottron2000.BusinessLogic
{
    public partial class LotteryNumbersGenerator
    {
        public static class WinningNumbers
        {
            #region VARIBALES ERROR HANDLING
            private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
            private static string ERROR_OCCURED_IN_CLASS = "LotteryNumbersGenerator.WinningNumbers";
            private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
            private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
            #endregion

            private static ILog _logger;
            public static void Init(ILog logger)
            {
                _logger = logger;
            }

            public static DrawWinningNumbersCollection GenerateRandonNumberSet(SimulatedDrawParameters simulatedDrawParams)
            {
                try
                {
                    LotteryNumbers lotteryNumbers = null;
                    RandomNumbers alexisRandomNumGenerator = new RandomNumbers();
                    List<int> randomIndexPool = null;

                    var randomIndexes = Base.GenerateRandomIndexes(simulatedDrawParams.WinningNumbers.SetsQuantity, simulatedDrawParams.DrawSubCategory);
                    DrawWinningNumbersCollection numbersCollection = new DrawWinningNumbersCollection();

                    #region MAIN LOTTO
                    List<LotteryNumbers> mainLottoNumbers = new List<LotteryNumbers>();
                    foreach (var numIndex in randomIndexes.MainLottoIndexes)
                    {
                        WinningNumberPermutation dsWinningPermutation = WinningNumberPermutationBL.GetByItemID(numIndex);
                        if (dsWinningPermutation != null)
                        {
                            lotteryNumbers = new LotteryNumbers(Guid.NewGuid().ToString(),dsWinningPermutation.Number1, dsWinningPermutation.Number2, dsWinningPermutation.Number3, dsWinningPermutation.Number4, dsWinningPermutation.Number5, dsWinningPermutation.Number6, 0);
                        }

                        mainLottoNumbers.Add(lotteryNumbers);
                    }
                    #endregion

                    #region LOTTO PLUS
                    List<LotteryNumbers> lottoPlus = new List<LotteryNumbers>();
                    foreach (var numIndex in randomIndexes.LottoPlusIndexes)
                    {
                        WinningNumberPermutation dsWinningPermutation = WinningNumberPermutationBL.GetByItemID(numIndex);
                        if (dsWinningPermutation != null)
                        {
                            lotteryNumbers = new LotteryNumbers(Guid.NewGuid().ToString(), dsWinningPermutation.Number1, dsWinningPermutation.Number2, dsWinningPermutation.Number3, dsWinningPermutation.Number4, dsWinningPermutation.Number5, dsWinningPermutation.Number6, 0);
                        }

                        lottoPlus.Add(lotteryNumbers);
                    }
                    #endregion

                    numbersCollection.LottoPlusNumbersCollection = lottoPlus.AsEnumerable();
                    numbersCollection.MainLottoNumbersCollection = mainLottoNumbers.AsEnumerable();

                    return numbersCollection;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomNumberSets";
                    string errorMethodSignature = "public static DrawWinningNumbersCollection GenerateRandonNumberSet(SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static DrawWinningNumbersCollection GenerateParmatizedRandonNumberSet(SimulatedDrawParameters simulatedDrawParams)
            {
                try
                {
                    LotteryNumbers lotteryNumbers = null;
                    RandomNumbers alexisRandomNumGenerator = new RandomNumbers();
                    List<int> randomIndexPool = null;

                    var localNumbersPool = WinningNumberPermutationBL.GetByRange(simulatedDrawParams.WinningNumbers.MinCheckSum, simulatedDrawParams.WinningNumbers.MaxCheckSum);
                    int poolCount = WinningNumberPermutationBL.CountGetByRange(simulatedDrawParams.WinningNumbers.MinCheckSum, simulatedDrawParams.WinningNumbers.MaxCheckSum);

                    var randomIndexes = Base.GenerateRandomIndexes(simulatedDrawParams.WinningNumbers.SetsQuantity, simulatedDrawParams.DrawSubCategory, poolCount);
                    DrawWinningNumbersCollection numbersCollection = new DrawWinningNumbersCollection();

                    #region MAIN LOTTO
                    List<LotteryNumbers> mainLottoNumbers = new List<LotteryNumbers>();
                    foreach (var numIndex in randomIndexes.MainLottoIndexes)
                    {
                        WinningNumberPermutation dsWinningPermutation = localNumbersPool.Skip(numIndex-1).Take(1).FirstOrDefault();
                        if (dsWinningPermutation != null)
                        {
                            int bonusNumber = Base.GenerateRandomBonusNumber(1).First();
                            lotteryNumbers = new LotteryNumbers(Guid.NewGuid().ToString(), dsWinningPermutation.Number1, dsWinningPermutation.Number2, dsWinningPermutation.Number3, dsWinningPermutation.Number4, dsWinningPermutation.Number5, dsWinningPermutation.Number6, bonusNumber);
                        }

                        mainLottoNumbers.Add(lotteryNumbers);
                    }
                    #endregion

                    #region LOTTO PLUS
                    List<LotteryNumbers> lottoPlus = new List<LotteryNumbers>();
                    foreach (var numIndex in randomIndexes.LottoPlusIndexes)
                    {
                        WinningNumberPermutation dsWinningPermutation = localNumbersPool.Skip(numIndex - 1).Take(1).FirstOrDefault();
                        if (dsWinningPermutation != null)
                        {
                            int bonusNumber = Base.GenerateRandomBonusNumber(1).First();
                            lotteryNumbers = new LotteryNumbers(Guid.NewGuid().ToString(), dsWinningPermutation.Number1, dsWinningPermutation.Number2, dsWinningPermutation.Number3, dsWinningPermutation.Number4, dsWinningPermutation.Number5, dsWinningPermutation.Number6, bonusNumber);
                        }

                        lottoPlus.Add(lotteryNumbers);
                    }
                    #endregion

                    numbersCollection.LottoPlusNumbersCollection = lottoPlus.AsEnumerable();
                    numbersCollection.MainLottoNumbersCollection = mainLottoNumbers.AsEnumerable();

                    return numbersCollection;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateParmatizedRandonNumberSet";
                    string errorMethodSignature = "public static GenerateParmatizedRandonNumberSet GenerateRandonNumberSet(SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }


            public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberSets(SimulatedDrawParameters simulatedDrawParams)
            {
                try
                {
                    return GetRandomHistoricalWinningNumberSets(simulatedDrawParams.WinningNumbers.SetsQuantity, simulatedDrawParams.DrawSubCategory, simulatedDrawParams.WinningNumbers.MinCheckSumCount, simulatedDrawParams.WinningNumbers.MaxCheckSumCount, simulatedDrawParams.WinningNumbers.HistoricalFrom, simulatedDrawParams.WinningNumbers.HistoricalTo);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandomHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberSets(SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int minCheckSumCount, int maxCheckSumCount, DateTime? fromDate = null, DateTime? toDate = null)
            {
                try
                {
                    RandomNumbers randomNumGenerator = new RandomNumbers();

                    #region Normalized Date Range
                    var normalizedDateRangeDictionary = LottronUtilities.DateRange.GetNormalizedRange(fromDate, toDate);
                    var normalizedDateRange = new NormilizedDateRange(
                        normalizedDateRangeDictionary[LottronConstants.CollectionKeys.DICTIONARY_DATE_FROM_DATE],
                        normalizedDateRangeDictionary[LottronConstants.CollectionKeys.DICTIONARY_DATE_TO_DATE]
                        );
                    #endregion

                    var lottoResultPool = SALottoResultBL.GetByRange(normalizedDateRange.FromDate, normalizedDateRange.ToDate, minCheckSumCount, maxCheckSumCount).ToList();
                    var lottoPlusResultPool = SALottoPlusResultBL.GetByRange(normalizedDateRange.FromDate, normalizedDateRange.ToDate, minCheckSumCount, maxCheckSumCount).ToList();


                    int mainLottoMaxQuantity = quantity; if (mainLottoMaxQuantity > lottoResultPool.Count) { mainLottoMaxQuantity = lottoResultPool.Count; }
                    var mainLottoIndexPool = randomNumGenerator.GetUniqueRandomNumbers(mainLottoMaxQuantity, 1, lottoResultPool.Count);

                    int lottoPlusMaxQuantity = quantity; if (lottoPlusMaxQuantity > lottoPlusResultPool.Count) { lottoPlusMaxQuantity = lottoPlusResultPool.Count; }
                    var lottoPlusIndexPool = randomNumGenerator.GetUniqueRandomNumbers(lottoPlusMaxQuantity, 1, lottoPlusResultPool.Count);


                    List<LotteryNumbers> mainLottoNumbers = new List<LotteryNumbers>();
                    List<LotteryNumbers> lottoPlusNumbers = new List<LotteryNumbers>();

                    foreach (var index in mainLottoIndexPool)
                    {
                        var lottoResult = lottoResultPool[index];
                        var lotteryNumber = SALottoResultBL.MapToLotteryNumbers(lottoResult);
                        lotteryNumber.TicketUniqueID = Guid.NewGuid().ToString();

                        mainLottoNumbers.Add(lotteryNumber);
                    }

                    foreach (var index in lottoPlusIndexPool)
                    {
                        var lottoPlusResult = lottoPlusResultPool[index];
                        var lotteryNumber = SALottoPlusResultBL.MapToLotteryNumbers(lottoPlusResult);
                        lotteryNumber.TicketUniqueID = Guid.NewGuid().ToString();

                        lottoPlusNumbers.Add(lotteryNumber);
                    }

                    DrawWinningNumbersCollection winningNumbers = new DrawWinningNumbersCollection();
                    winningNumbers.MainLottoNumbersCollection = mainLottoNumbers.AsEnumerable();
                    winningNumbers.LottoPlusNumbersCollection = lottoPlusNumbers.AsEnumerable();

                    return winningNumbers;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandomHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static DrawWinningNumbersCollection GenerateRandonNumberSet(SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }
            //overloads
        }
    }
}