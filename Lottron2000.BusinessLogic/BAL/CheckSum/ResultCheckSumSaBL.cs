using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class ResultCheckSumSaBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "ResultCheckSumSaBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IResultCheckSumSaRepository _resultCheckSumSaRepository;
        private static ILog _logger;

        public static void Init(IResultCheckSumSaRepository resultCheckSumSaRepository, ILog logger)
        {
            _resultCheckSumSaRepository = resultCheckSumSaRepository;
            _logger = logger;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<ResultCheckSumSa> GetAll()
        {
            return _resultCheckSumSaRepository.GetAll();
        }

        public static ResultCheckSumSa GetByID(string ResultCheckSumSaID)
        {
            return _resultCheckSumSaRepository.GetByID(ResultCheckSumSaID);
        }

        public static ResultCheckSumSa GetByItemID(int itemID)
        {
            return _resultCheckSumSaRepository.GetByItemID(itemID);
        }

        public static ResultCheckSumSa GetByCheckSum(int checkSum)
        {
            return _resultCheckSumSaRepository.GetByCheckSum(checkSum);
        }

        public static void Insert(ResultCheckSumSa theResultCheckSumSa)
        {
            #region IMPLEMENTATION
            try
            {
                _resultCheckSumSaRepository.Insert(theResultCheckSumSa);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_resultCheckSumSaRepository theResultCheckSumSa)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(ResultCheckSumSa theResultCheckSumSa)
        {
            #region IMPLEMENTATION
            try
            {
                _resultCheckSumSaRepository.Delete(theResultCheckSumSa);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_resultCheckSumSaRepository theResultCheckSumSa)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string ResultCheckSumSaID)
        {
            #region IMPLEMENTATION
            try
            {
                //_resultCheckSumSaRepository.DeleteByID(ResultCheckSumSaID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string ResultCheckSumSaID)";
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