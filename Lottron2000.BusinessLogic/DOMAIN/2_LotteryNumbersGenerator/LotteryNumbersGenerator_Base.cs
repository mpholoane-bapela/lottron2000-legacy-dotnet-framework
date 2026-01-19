
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
        public static class Base
        {
            #region VARIBALES ERROR HANDLING
            private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
            private static string ERROR_OCCURED_IN_CLASS = "LotteryNumbersGenerator.Base";
            private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
            private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
            #endregion

            private static ILog _logger;

            public static void Init(ILog logger)
            {
                _logger = logger;
            }

            #region RANDOM
            public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity)
            {
                try
                {
                    List<LotteryNumbers> winningNumbers = new List<LotteryNumbers>();

                    RandomNumbers randomNumbers = new RandomNumbers();
                    var uniqueIndexes = randomNumbers.GetUniqueRandomNumbers(quantity, 1, LottronConstants.WinningNumberPermutations.TotalPermutations);
                    var uniqueBonusNumbers = GenerateRandomBonusNumber(quantity);

                    int loopIndex = 0;

                    foreach (int index in uniqueIndexes)
                    {
                        var dsWinningNumber = WinningNumberPermutationBL.GetByItemID(index);
                        var bonusNumber = uniqueBonusNumbers[loopIndex];

                        LotteryNumbers playingNumbers = new LotteryNumbers(Guid.NewGuid().ToString(), dsWinningNumber.Number1, dsWinningNumber.Number2, dsWinningNumber.Number3, dsWinningNumber.Number4, dsWinningNumber.Number5, dsWinningNumber.Number6, bonusNumber);
                        winningNumbers.Add(playingNumbers);

                        loopIndex++;
                    }

                    return winningNumbers;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomNumberSets";
                    string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }


            public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity,int minCheckSum,int maxCheckSum)
            {
                try
                {
                    List<LotteryNumbers> winningNumbers = new List<LotteryNumbers>();
                    int totalCount = quantity;

                    var allNumbersPool = WinningNumberPermutationBL.GetByRange(minCheckSum, maxCheckSum).ToList();

                    RandomNumbers randomNumbers = new RandomNumbers();
                    var uniqueIndexes = randomNumbers.GetUniqueRandomNumbers(quantity, 1, allNumbersPool.Count() - 1);
                    var uniqueBonusNumbers = GenerateRandomBonusNumber(quantity);

                    int loopIndex = 0;

                    foreach (int index in uniqueIndexes)
                    {
                        var dsWinningNumber = allNumbersPool.ElementAt(index);
                        var bonusNumber = uniqueBonusNumbers[loopIndex];

                        LotteryNumbers playingNumbers = new LotteryNumbers(Guid.NewGuid().ToString(), dsWinningNumber.Number1, dsWinningNumber.Number2, dsWinningNumber.Number3, dsWinningNumber.Number4, dsWinningNumber.Number5, dsWinningNumber.Number6, bonusNumber);
                        winningNumbers.Add(playingNumbers);

                        loopIndex++;
                    }

                    return winningNumbers;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomNumberSets";
                    string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static List<int> GenerateRandomBonusNumber(int quantity)
            {
                try
                {
                    RandomNumbers randomNumbers = new RandomNumbers();
                    return randomNumbers.GetRandomNumbers(quantity, 1, 49);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomBonusNumber";
                    string errorMethodSignature = "private static List<int> GenerateRandomBonusNumber(int quantity)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }
            #endregion


            #region HISTORICAL
            public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberCollection(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategoryPool, int minCheckSum, int maxCheckSum, DateTime? fromDate = null, DateTime? toDate = null)
            {
                try
                {
                    var normalizedDateRangeDictionary = LottronUtilities.DateRange.GetNormalizedRange(fromDate, toDate);
                    var normalizedDateRange = new NormilizedDateRange(
                        normalizedDateRangeDictionary[LottronConstants.CollectionKeys.DICTIONARY_DATE_FROM_DATE],
                        normalizedDateRangeDictionary[LottronConstants.CollectionKeys.DICTIONARY_DATE_TO_DATE]
                        );

                    var mainLottoNumbers = SALottoResultBL.GetByRange(normalizedDateRange.FromDate, normalizedDateRange.ToDate, minCheckSum, maxCheckSum).Take(quantity);
                    var mappedMainLottoNumbers = SALottoResultBL.MapToLotteryNumbers(mainLottoNumbers);

                    var testItemA = mainLottoNumbers.FirstOrDefault().SALottoResultCheckSums;
                    var testItem = mainLottoNumbers.FirstOrDefault().SALottoResultCheckSums.FirstOrDefault().ResultCheckSumSa.Count;

                    IEnumerable<LotteryNumbers> mappedLottoPlusNumbers = null;
                    if (drawSubCategoryPool == LottronConstants.PlayingSession.DrawSubCategory.LottoPlus)
                    {
                        var lottoPlusNumbers = SALottoPlusResultBL.GetByRange(normalizedDateRange.FromDate, normalizedDateRange.ToDate, minCheckSum, maxCheckSum).Take(quantity);
                        mappedLottoPlusNumbers = SALottoPlusResultBL.MapToLotteryNumbers(lottoPlusNumbers);
                    }

                    DrawWinningNumbersCollection winningNumbersCollection = new DrawWinningNumbersCollection(mappedMainLottoNumbers, mappedLottoPlusNumbers);

                    return winningNumbersCollection;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandomHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, DateTime? fromDate = null, DateTime? toDate = null)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberCollection(decimal budget, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int minCheckSum, int maxCheckSum, DateTime? fromDate = null, DateTime? toDate = null)
            {
                try
                {
                    int quantity = CalculateNumberOfSlotsForBudget(budget, drawSubCategory);
                    return GetRandomHistoricalWinningNumberCollection(quantity, drawSubCategory, minCheckSum, maxCheckSum, fromDate, toDate);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandonmHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static DrawWinningNumbersCollection GetRandonmHistoricalWinningNumberSets(decimal budget, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, DateTime? fromDate = null, DateTime? toDate = null)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }


            public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategoryPool, int minCheckSum, int maxCheckSum, DateTime? fromDate = null, DateTime? toDate = null)
            {
                try
                {
                    DrawWinningNumbersCollection winningNumbersCollection = GetRandomHistoricalWinningNumberCollection(quantity, drawSubCategoryPool, minCheckSum, maxCheckSum, fromDate, toDate);
                    List<LotteryNumbers> numberSets = new List<LotteryNumbers>();

                    numberSets.AddRange(winningNumbersCollection.MainLottoNumbersCollection);
                    if (drawSubCategoryPool == LottronConstants.PlayingSession.DrawSubCategory.LottoPlus)
                    {
                        numberSets.AddRange(winningNumbersCollection.LottoPlusNumbersCollection);
                    }

                    numberSets.Where(a => a.CheckSum >= minCheckSum && a.CheckSum <= maxCheckSum);

                    return numberSets;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandomHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static DrawWinningNumbersCollection GetRandomHistoricalWinningNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, DateTime? fromDate = null, DateTime? toDate = null)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }
            #endregion


            #region OTHER
            public static int CalculateNumberOfSlotsForBudget(decimal budget, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
            {
                try
                {
                    var ticketPrice = SATicketPriceBL.GetByDrawSubCategory(drawSubCategory);
                    decimal pricePerSlot = (decimal)ticketPrice.TicketPrice;
                    int numSlots = (int)Math.Floor((budget / pricePerSlot));

                    return numSlots;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "CalculateNumberOfSlotsForBudget";
                    string errorMethodSignature = "public static int CalculateNumberOfSlotsForBudget(decimal budget, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return 0;
                }
                #endregion
            }

            public static RandomIndexes GenerateRandomIndexes(int quantityPerSet, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int maxPermutations = 0)
            {
                try
                {
                    List<int> indexPool = new List<int>();
                    int indexSize = quantityPerSet;
                    int mainLottoItemsCount = indexSize;
                    
                    if (drawSubCategory == LottronConstants.PlayingSession.DrawSubCategory.LottoPlus)
                    {
                        indexSize = indexSize * 2;
                    }

                    RandomNumbers randomNumGenerator = new RandomNumbers();
                    
                    int maxNumbers = maxPermutations;
                    if(maxPermutations==0)
                    {
                        maxNumbers = LottronConstants.WinningNumberPermutations.TotalPermutations;
                    }
                    indexPool = randomNumGenerator.GetUniqueRandomNumbers(indexSize, 1, maxNumbers);

                    RandomIndexes randomIndexes = new RandomIndexes();
                    randomIndexes.MainLottoIndexes = indexPool.Take(mainLottoItemsCount).ToList();
                    randomIndexes.LottoPlusIndexes = indexPool.Skip(mainLottoItemsCount).Take(mainLottoItemsCount).ToList();//take the second half
                    
                    return randomIndexes;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomIndexes";
                    string errorMethodSignature = "public static RandomIndexes GenerateRandomIndexes(int quantityPerSet, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }
            #endregion
        }

        public class RandomIndexes
        {
            public List<int> MainLottoIndexes { get; set; }
            public List<int> LottoPlusIndexes { get; set; }

            public RandomIndexes()
            {
                MainLottoIndexes = null;
                LottoPlusIndexes = null;
            }
        }

    }
}