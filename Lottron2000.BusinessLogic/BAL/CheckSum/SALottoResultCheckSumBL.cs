using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class SALottoResultCheckSumBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SALottoResultCheckSumBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISALottoResultCheckSumRepository _sALottoResultCheckSumRepository;
        private static ILog _logger;

        public static void Init(ISALottoResultCheckSumRepository sALottoResultCheckSumRepository, ILog logger)
        {
            _logger = logger;
            _sALottoResultCheckSumRepository = sALottoResultCheckSumRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SALottoResultCheckSum> GetAll()
        {
            return _sALottoResultCheckSumRepository.GetAll();
        }

        public static SALottoResultCheckSum GetByID(string SALottoResultCheckSumID)
        {
            return _sALottoResultCheckSumRepository.GetByID(SALottoResultCheckSumID);
        }

        public static SALottoResultCheckSum GetByItemID(int itemID)
        {
            return _sALottoResultCheckSumRepository.GetByItemID(itemID);
        }

        public static void Insert(SALottoResultCheckSum theSALottoResultCheckSum)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoResultCheckSumRepository.Insert(theSALottoResultCheckSum);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_resultCheckSumSaRepository theSALottoResultCheckSum)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SALottoResultCheckSum theSALottoResultCheckSum)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoResultCheckSumRepository.Delete(theSALottoResultCheckSum);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_resultCheckSumSaRepository theSALottoResultCheckSum)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string SALottoResultCheckSumID)
        {
            #region IMPLEMENTATION
            try
            {
                //_sALottoResultCheckSumRepository.DeleteByID(SALottoResultCheckSumID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string SALottoResultCheckSumID)";
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