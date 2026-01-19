using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Models;
using Lottron2000.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class SimulatedDrawPrizeBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawPrizeBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISimulatedDrawPrizeRepository _simulatedDrawPrizeRepository;
        private static ILog _logger;

        public static void Init(ISimulatedDrawPrizeRepository simulatedDrawPrizeRepository, ILog logger)
        {
            _logger = logger;
            _simulatedDrawPrizeRepository = simulatedDrawPrizeRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SimulatedDrawPrize> GetAll()
        {
            return _simulatedDrawPrizeRepository.GetAll();
        }

        public static SimulatedDrawPrize GetByID(string SimulatedDrawPrizeID)
        {
            return _simulatedDrawPrizeRepository.GetByID(SimulatedDrawPrizeID);
        }

        public static SimulatedDrawPrize GetByItemID(int itemID)
        {
            return _simulatedDrawPrizeRepository.GetByItemID(itemID);
        }

        public static void Insert(SimulatedDrawPrize theSimulatedDrawPrize)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawPrizeRepository.Insert(theSimulatedDrawPrize);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawPrizeRepository theSimulatedDrawPrize)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SimulatedDrawPrize theSimulatedDrawPrize)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawPrizeRepository.Delete(theSimulatedDrawPrize);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_simulatedDrawPrizeRepository theSimulatedDrawPrize)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string SimulatedDrawPrizeID)
        {
            #region IMPLEMENTATION
            try
            {
                //_simulatedDrawPrizeRepository.DeleteByID(SimulatedDrawPrizeID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string SimulatedDrawPrizeID)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        #endregion

        public static void SaveDrawWinningPrizeSet(string simulatedDrawID, DrawWinningPrizeSet drawWinningPrizeSet)
        {
            #region IMPLEMENTATION
            try
            {
                // Save Main Lotto
                #region MAIN LOTTO
                var mainLottoData = drawWinningPrizeSet.MainLottoPrizeAndWinner;
                
                SimulatedDrawPrize mainLottoDrawPrize = new SimulatedDrawPrize();
                mainLottoDrawPrize.Created = DateTime.Now;
                mainLottoDrawPrize.SimulatedDrawID = simulatedDrawID;
                mainLottoDrawPrize.SimulatedDrawPrizeID = Guid.NewGuid().ToString();
                mainLottoDrawPrize.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.MainLotto.ToString();

                mainLottoDrawPrize.Div1 = mainLottoData.WinningPrize.Div1;
                mainLottoDrawPrize.Div1Winners = mainLottoData.WinningPrizeWinner.Div1Winners;

                mainLottoDrawPrize.Div2 = mainLottoData.WinningPrize.Div2;
                mainLottoDrawPrize.Div2Winners = mainLottoData.WinningPrizeWinner.Div2Winners;

                mainLottoDrawPrize.Div3 = mainLottoData.WinningPrize.Div3;
                mainLottoDrawPrize.Div3Winners = mainLottoData.WinningPrizeWinner.Div3Winners;

                mainLottoDrawPrize.Div4 = mainLottoData.WinningPrize.Div4;
                mainLottoDrawPrize.Div4Winners = mainLottoData.WinningPrizeWinner.Div4Winners;

                mainLottoDrawPrize.Div5 = mainLottoData.WinningPrize.Div5;
                mainLottoDrawPrize.Div5Winners = mainLottoData.WinningPrizeWinner.Div5Winners;

                mainLottoDrawPrize.Div6 = mainLottoData.WinningPrize.Div6;
                mainLottoDrawPrize.Div6Winners = mainLottoData.WinningPrizeWinner.Div6Winners;

                mainLottoDrawPrize.Div7 = mainLottoData.WinningPrize.Div7;
                mainLottoDrawPrize.Div7Winners = mainLottoData.WinningPrizeWinner.Div7Winners;

                Insert(mainLottoDrawPrize);
                #endregion


                // Save Lotto Plus
                #region LOTTO PLUS
                var lottoPlusData = drawWinningPrizeSet.LottoPlusPrizeAndWinner;

                SimulatedDrawPrize lottoPlusDrawPrize = new SimulatedDrawPrize();
                lottoPlusDrawPrize.Created = DateTime.Now;
                lottoPlusDrawPrize.SimulatedDrawID = simulatedDrawID;
                lottoPlusDrawPrize.SimulatedDrawPrizeID = Guid.NewGuid().ToString();
                lottoPlusDrawPrize.DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus.ToString();

                lottoPlusDrawPrize.Div1 = lottoPlusData.WinningPrize.Div1;
                lottoPlusDrawPrize.Div1Winners = lottoPlusData.WinningPrizeWinner.Div1Winners;

                lottoPlusDrawPrize.Div2 = lottoPlusData.WinningPrize.Div2;
                lottoPlusDrawPrize.Div2Winners = lottoPlusData.WinningPrizeWinner.Div2Winners;

                lottoPlusDrawPrize.Div3 = lottoPlusData.WinningPrize.Div3;
                lottoPlusDrawPrize.Div3Winners = lottoPlusData.WinningPrizeWinner.Div3Winners;

                lottoPlusDrawPrize.Div4 = lottoPlusData.WinningPrize.Div4;
                lottoPlusDrawPrize.Div4Winners = lottoPlusData.WinningPrizeWinner.Div4Winners;

                lottoPlusDrawPrize.Div5 = lottoPlusData.WinningPrize.Div5;
                lottoPlusDrawPrize.Div5Winners = lottoPlusData.WinningPrizeWinner.Div5Winners;

                mainLottoDrawPrize.Div6 = lottoPlusData.WinningPrize.Div6;
                lottoPlusDrawPrize.Div6Winners = lottoPlusData.WinningPrizeWinner.Div6Winners;

                lottoPlusDrawPrize.Div7 = lottoPlusData.WinningPrize.Div7;
                lottoPlusDrawPrize.Div7Winners = lottoPlusData.WinningPrizeWinner.Div7Winners;

                Insert(lottoPlusDrawPrize);
                #endregion
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "SavePrizeSet";
                string errorMethodSignature = "public static void SavePrizeSet(DrawWinningPrizeSet drawWinningPrizeSet)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        /*
*/
    }
}