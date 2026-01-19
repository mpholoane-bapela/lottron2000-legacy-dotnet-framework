using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class WinningNumberPermutationBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "WinningNumberPermutationBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IWinningNumberPermutationRepository _winningNumberPermutationRepository;
        private static ILog _logger;

        public static void Init(IWinningNumberPermutationRepository winningNumberPermutationRepository, ILog logger)
        {
            _logger = logger;
            _winningNumberPermutationRepository = winningNumberPermutationRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<WinningNumberPermutation> GetAll()
        {
            return _winningNumberPermutationRepository.GetAll();
        }

        //public static IQueryable<WinningNumberPermutation> GetByID(string WinningNumberPermutationID)
        //{
        //    return _winningNumberPermutationRepository.GetByID(WinningNumberPermutationID);
        //}

        public static WinningNumberPermutation GetByItemID(int itemID)
        {
            return _winningNumberPermutationRepository.GetByItemID(itemID);
        }

        public static IQueryable<WinningNumberPermutation> GetByRange(int minCheckSum,int maxCheckSum)
        {
            return _winningNumberPermutationRepository.GetByRange(minCheckSum, maxCheckSum);
        }

        public static int CountGetByRange(int minCheckSum, int maxCheckSum)
        {
            return _winningNumberPermutationRepository.CountGetByRange(minCheckSum, maxCheckSum);
        }

        public static void Insert(WinningNumberPermutation theWinningNumberPermutation)
        {
            #region IMPLEMENTATION
            try
            {
                _winningNumberPermutationRepository.Insert(theWinningNumberPermutation);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_drawPayoutRepository theWinningNumberPermutation)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(WinningNumberPermutation theWinningNumberPermutation)
        {
            #region IMPLEMENTATION
            try
            {
                _winningNumberPermutationRepository.Delete(theWinningNumberPermutation);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_drawPayoutRepository theWinningNumberPermutation)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string WinningNumberPermutationID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _winningNumberPermutationRepository.DeleteByID(WinningNumberPermutationID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string WinningNumberPermutationID)";
        //        string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
        //        _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
        //    }
        //    #endregion
        //}

        #endregion

        /*
*/
    }
}

