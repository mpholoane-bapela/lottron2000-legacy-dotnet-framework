using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottron2000.Models;
using Lottron2000.BusinessLogic;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Ydin;
using Lottron2000.Ydin.Utilities;

namespace Lottron2000.BusinessLogic
{
    public partial class LotteryNumbersGenerator
    {
        public static class PlayingTickets
        {
            #region VARIBALES ERROR HANDLING
            private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
            private static string ERROR_OCCURED_IN_CLASS = "LotteryNumbersGenerator.PlayingTicketNumbers";
            private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
            private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
            #endregion

            private static ILog _logger;
            public static void Init(ILog logger)
            {
                _logger = logger;
            }

            #region RANDOM NUMBER SETS
            public static List<LotteryNumbers> GenerateRandomNumberSets(SimulatedDrawParameters simulatedDrawParams)
            {
                try
                {
                    if ((int)simulatedDrawParams.PlayingTickets.Budget > 0)
                    {
                        return GenerateRandomNumberSets(simulatedDrawParams.PlayingTickets.Budget, simulatedDrawParams.DrawSubCategory);
                    }

                    else
                    {
                        return GenerateRandomNumberSets(simulatedDrawParams.PlayingTickets.Quantity, simulatedDrawParams.DrawSubCategory);
                    }
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

            public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity)
            {
                try
                {
                    return LotteryNumbersGenerator.Base.GenerateRandomNumberSets(quantity);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomNumberSets";
                    string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static List<LotteryNumbers> GenerateRandomNumberSets(decimal budget, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
            {
                try
                {
                    int quantity = Base.CalculateNumberOfSlotsForBudget(budget, drawSubCategory);
                    return Base.GenerateRandomNumberSets(quantity);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomNumberSets";
                    string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }


            public static List<LotteryNumbers> GenerateRandomParamatizedNumberSets(SimulatedDrawParameters simulatedDrawParam)
            {
                try
                {
                    int quantity = Base.CalculateNumberOfSlotsForBudget(simulatedDrawParam.PlayingTickets.Budget, simulatedDrawParam.DrawSubCategory);
                    return Base.GenerateRandomNumberSets(quantity, simulatedDrawParam.PlayingTickets.MinCheckSum, simulatedDrawParam.PlayingTickets.MaxCheckSum);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GenerateRandomParamatizedNumberSets";
                    string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }


            #endregion

            #region WINNING NUMBER SETS
            public static List<LotteryNumbers> GetRandomHistoricalWinningNumberSets(SimulatedDrawParameters simulatedDrawParams)
            {
                try
                {
                    int quantity = Base.CalculateNumberOfSlotsForBudget(simulatedDrawParams.PlayingTickets.Budget, simulatedDrawParams.DrawSubCategory);
                    
                    var collection = Base.GetRandomHistoricalWinningNumberCollection(
                        quantity                          
                        , simulatedDrawParams.DrawSubCategory
                        , simulatedDrawParams.PlayingTickets.MinCheckSumCount
                        , simulatedDrawParams.PlayingTickets.MaxCheckSumCount
                        , simulatedDrawParams.PlayingTickets.HistoricalFrom
                        , simulatedDrawParams.PlayingTickets.HistoricalTo
                        );

                    return GetMergedNumberSets(collection, simulatedDrawParams.DrawSubCategory, simulatedDrawParams.PlayingTickets.MinCheckSumCount, simulatedDrawParams.PlayingTickets.MaxCheckSumCount);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandomHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static void PlayTheLottery(string playingSessionID, SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            private static List<LotteryNumbers> GetMergedNumberSets(DrawWinningNumbersCollection numbersCollection, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory,int minCheckSum,int maxCheckSum)
            {
                try
                {
                    List<LotteryNumbers> lotteryNumberSets = new List<LotteryNumbers>();

                    lotteryNumberSets.AddRange(numbersCollection.MainLottoNumbersCollection);

                    if (drawSubCategory == LottronConstants.PlayingSession.DrawSubCategory.LottoPlus)
                    {
                        lotteryNumberSets.AddRange(numbersCollection.LottoPlusNumbersCollection);
                        lotteryNumberSets = lotteryNumberSets.Where(a => a.CheckSumCount >= minCheckSum && a.CheckSumCount <= maxCheckSum).ToList();
                    }
                    return lotteryNumberSets;
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetMergedNumberSets";
                    string errorMethodSignature = "private static List<LotteryNumbers> GetMergedNumberSets(DrawWinningNumbersCollection numbersCollection, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory,int minCheckSum,int maxCheckSum)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static List<LotteryNumbers> GetRandomHistoricalWinningNumberSets(int quantity, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int minCheckSum, int maxCheckSum, DateTime? fromDate = null, DateTime? toDate = null)
            {
                try
                {
                     var collection = Base.GetRandomHistoricalWinningNumberCollection(quantity, drawSubCategory, minCheckSum, maxCheckSum, fromDate, toDate);
                     return GetMergedNumberSets(collection, drawSubCategory, minCheckSum, maxCheckSum);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandomHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static void PlayTheLottery(string playingSessionID, SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }

            public static List<LotteryNumbers> GetRandonHistoricalWinningNumberSets(decimal budget, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int minCheckSumCount, int maxCheckSumCount, DateTime? fromDate = null, DateTime? toDate = null)
            {
                try
                {
                    int quantity = Base.CalculateNumberOfSlotsForBudget(budget, drawSubCategory);
                    var collection = Base.GetRandomHistoricalWinningNumberCollection(quantity, drawSubCategory, minCheckSumCount, maxCheckSumCount, fromDate, toDate);
                    return GetMergedNumberSets(collection, drawSubCategory, minCheckSumCount, maxCheckSumCount);
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetRandonmHistoricalWinningNumberSets";
                    string errorMethodSignature = "public static void PlayTheLottery(string playingSessionID, SimulatedDrawParameters simulatedDrawParams)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            }
            #endregion

        }
    }
}