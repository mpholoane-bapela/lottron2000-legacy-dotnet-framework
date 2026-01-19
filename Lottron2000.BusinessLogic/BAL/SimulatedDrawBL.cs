using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class SimulatedDrawBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISimulatedDrawRepository _simulatedDrawRepository;
        private static ILog _logger;

        public static void Init(ISimulatedDrawRepository simulatedDrawRepository, ILog logger)
        {
            _logger = logger;
            _simulatedDrawRepository = simulatedDrawRepository;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SimulatedDraw> GetAll()
        {
            return _simulatedDrawRepository.GetAll();
        }

        public static SimulatedDraw GetByID(string SimulatedDrawID)
        {
            return _simulatedDrawRepository.GetByID(SimulatedDrawID);
        }

        public static SimulatedDraw GetByItemID(int itemID)
        {
            return _simulatedDrawRepository.GetByItemID(itemID);
        }

        public static void Insert(SimulatedDraw theSimulatedDraw)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawRepository.Insert(theSimulatedDraw);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawRepository theSimulatedDraw)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static SimulatedDraw Create(string playingSessionID, SimulatedDraw theSimulatedDraw)
        {
            #region IMPLEMENTATION
            try
            {
                theSimulatedDraw.Created = DateTime.Now;
                theSimulatedDraw.SimulatedDrawID = Guid.NewGuid().ToString();
                theSimulatedDraw.PlayingSessionID = playingSessionID;

                _simulatedDrawRepository.Insert(theSimulatedDraw);
                return GetByID(theSimulatedDraw.SimulatedDrawID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawRepository theSimulatedDraw)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                return null;
            }
            #endregion
        }

        public static void Delete(SimulatedDraw theSimulatedDraw)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawRepository.Delete(theSimulatedDraw);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_simulatedDrawRepository theSimulatedDraw)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string SimulatedDrawID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _simulatedDrawRepository.DeleteByID(SimulatedDrawID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string SimulatedDrawID)";
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