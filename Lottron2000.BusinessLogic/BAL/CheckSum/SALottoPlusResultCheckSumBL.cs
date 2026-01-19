using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class SALottoPlusResultCheckSumBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SALottoPlusResultCheckSumBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISALottoPlusResultCheckSumRepository _sALottoPlusResultCheckSumRepository;
        private static ILog _logger;

        public static void Init(ISALottoPlusResultCheckSumRepository sALottoPlusResultCheckSumRepository, ILog logger)
        {
            _logger = logger;
            _sALottoPlusResultCheckSumRepository = sALottoPlusResultCheckSumRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SALottoPlusResultCheckSum> GetAll()
        {
            return _sALottoPlusResultCheckSumRepository.GetAll();
        }

        public static SALottoPlusResultCheckSum GetByID(string SALottoPlusResultCheckSumID)
        {
            return _sALottoPlusResultCheckSumRepository.GetByID(SALottoPlusResultCheckSumID);
        }

        public static SALottoPlusResultCheckSum GetByItemID(int itemID)
        {
            return _sALottoPlusResultCheckSumRepository.GetByItemID(itemID);
        }

        public static void Insert(SALottoPlusResultCheckSum theSALottoPlusResultCheckSum)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoPlusResultCheckSumRepository.Insert(theSALottoPlusResultCheckSum);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_resultCheckSumSaRepository theSALottoPlusResultCheckSum)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SALottoPlusResultCheckSum theSALottoPlusResultCheckSum)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoPlusResultCheckSumRepository.Delete(theSALottoPlusResultCheckSum);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_resultCheckSumSaRepository theSALottoPlusResultCheckSum)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string SALottoPlusResultCheckSumID)
        {
            #region IMPLEMENTATION
            try
            {
                //_sALottoPlusResultCheckSumRepository.DeleteByID(SALottoPlusResultCheckSumID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string SALottoPlusResultCheckSumID)";
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