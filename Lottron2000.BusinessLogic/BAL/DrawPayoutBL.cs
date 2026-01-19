using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class DrawPayoutBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "DrawPayoutBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IDrawPayoutRepository _drawPayoutRepository;
        private static ILog _logger;

        public static void Init(IDrawPayoutRepository drawPayoutRepository, ILog logger)
        {
            _drawPayoutRepository = drawPayoutRepository;
            _logger = logger;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<DrawPayout> GetAll()
        {
            return _drawPayoutRepository.GetAll();
        }

        //public static IQueryable<DrawPayout> GetByID(string DrawPayoutID)
        //{
        //    return _drawPayoutRepository.GetByID(DrawPayoutID);
        //}

        public static DrawPayout GetByItemID(int itemID)
        {
            return _drawPayoutRepository.GetByItemID(itemID);
        }

        public static void Insert(DrawPayout theDrawPayout)
        {
            #region IMPLEMENTATION
            try
            {
                _drawPayoutRepository.Insert(theDrawPayout);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_drawPayoutRepository theDrawPayout)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(DrawPayout theDrawPayout)
        {
            #region IMPLEMENTATION
            try
            {
                _drawPayoutRepository.Delete(theDrawPayout);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_drawPayoutRepository theDrawPayout)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string DrawPayoutID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _drawPayoutRepository.DeleteByID(DrawPayoutID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string DrawPayoutID)";
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