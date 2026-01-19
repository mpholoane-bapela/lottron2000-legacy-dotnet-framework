using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class WinningChanceBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "WinningChanceBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IWinningChanceRepository _winningChanceRepository;
        private static ILog _logger;

        public static void Init(IWinningChanceRepository winningChanceRepository, ILog logger)
        {
            _logger = logger;
            _winningChanceRepository = winningChanceRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<WinningChance> GetAll()
        {
            return _winningChanceRepository.GetAll();
        }

        //public static WinningChance GetByID(int id)
        //{
        //    return _winningChanceRepository.GetByID(id);
        //}

        public static WinningChance GetByItemID(int itemID)
        {
            return _winningChanceRepository.GetByItemID(itemID);
        }

        public static void Insert(WinningChance theWinningChance)
        {
            #region IMPLEMENTATION
            try
            {
                _winningChanceRepository.Insert(theWinningChance);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_drawPayoutRepository theWinningChance)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(WinningChance theWinningChance)
        {
            #region IMPLEMENTATION
            try
            {
                _winningChanceRepository.Delete(theWinningChance);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_drawPayoutRepository theWinningChance)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string WinningChanceID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _winningChanceRepository.DeleteByID(WinningChanceID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string WinningChanceID)";
        //        string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
        //        _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
        //    }
        //    #endregion
        //}


        public static void Update(WinningChance theWinningChance)
        {
            #region IMPLEMENTATION
            try
            {
                _winningChanceRepository.Update(theWinningChance);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Update";
                string errorMethodSignature = "public static void Update(WinningChance theWinningChance)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        #endregion

        /*
*/
    }
}

