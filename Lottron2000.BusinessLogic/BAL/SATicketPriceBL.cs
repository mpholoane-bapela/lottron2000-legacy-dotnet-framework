using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class SATicketPriceBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SATicketPriceBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISATicketPriceRepository _sATicketPriceRepository;
        private static ILog _logger;

        public static void Init(ISATicketPriceRepository sATicketPriceRepository, ILog logger)
        {
            _logger = logger;
            _sATicketPriceRepository = sATicketPriceRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SATicketPrice> GetAll()
        {
            return _sATicketPriceRepository.GetAll();
        }

        //public static IQueryable<SATicketPrice> GetByID(string SATicketPriceID)
        //{
        //    return _sATicketPriceRepository.GetByID(SATicketPriceID);
        //}

        public static SATicketPrice GetByItemID(int itemID)
        {
            return _sATicketPriceRepository.GetByItemID(itemID);
        }

        public static SATicketPrice GetByDrawSubCategory(LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
        {
            return _sATicketPriceRepository.GetByDrawSubCategory(drawSubCategory);
        }

        public static void Insert(SATicketPrice theSATicketPrice)
        {
            #region IMPLEMENTATION
            try
            {
                _sATicketPriceRepository.Insert(theSATicketPrice);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_sATicketPriceRepository theSATicketPrice)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SATicketPrice theSATicketPrice)
        {
            #region IMPLEMENTATION
            try
            {
                _sATicketPriceRepository.Delete(theSATicketPrice);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_sATicketPriceRepository theSATicketPrice)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string SATicketPriceID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _sATicketPriceRepository.DeleteByID(SATicketPriceID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string SATicketPriceID)";
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