using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;
using AutoMapper;
using Lottron2000.BusinessLogic.BAL.AutoMappers;
using Lottron2000.Models;

namespace Lottron2000.BusinessLogic
{
    public static class SimulatedDrawTicketResultBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawTicketResultBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISimulatedDrawTicketResultRepository _simulatedDrawTicketResultRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(ISimulatedDrawTicketResultRepository simulatedDrawTicketResultRepository, ILog logger)
        {
            _logger = logger;
            _simulatedDrawTicketResultRepository = simulatedDrawTicketResultRepository;

            _mapper = Mapper.Engine;
            SimulatedDrawTicketResult_AutoMapperConfig.CreateDomainToDbMapping();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SimulatedDrawTicketResult> GetAll()
        {
            return _simulatedDrawTicketResultRepository.GetAll();
        }

        //public static IQueryable<SimulatedDrawTicketResult> GetByID(string SimulatedDrawTicketResultID)
        //{
        //    return _simulatedDrawTicketResultRepository.GetByID(SimulatedDrawTicketResultID);
        //}

        public static SimulatedDrawTicketResult GetByItemID(int itemID)
        {
            return _simulatedDrawTicketResultRepository.GetByItemID(itemID);
        }

        public static void Insert(SimulatedDrawTicketResult theSimulatedDrawTicketResult)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawTicketResultRepository.Insert(theSimulatedDrawTicketResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawTicketResultRepository theSimulatedDrawTicketResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SimulatedDrawTicketResult theSimulatedDrawTicketResult)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawTicketResultRepository.Delete(theSimulatedDrawTicketResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_simulatedDrawTicketResultRepository theSimulatedDrawTicketResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string SimulatedDrawTicketResultID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _simulatedDrawTicketResultRepository.DeleteByID(SimulatedDrawTicketResultID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string SimulatedDrawTicketResultID)";
        //        string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
        //        _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
        //    }
        //    #endregion
        //}

        #endregion


        public static SimulatedDrawTicketResult MapLotteryNumbersToDb(DrawTicketMatch drawTicket)
        {
            return _mapper.Map<DrawTicketMatch, SimulatedDrawTicketResult>(drawTicket);
        }

        public static void SaveDrawTicketMatchToDb(DrawTicketMatch drawTicket)
        {
            #region IMPLEMENTATION
            try
            {
                SimulatedDrawTicketResult simulatedDrawTicketResult = MapLotteryNumbersToDb(drawTicket);
                simulatedDrawTicketResult.Created = DateTime.Now;
                simulatedDrawTicketResult.SimulatedDrawTicketResultID = Guid.NewGuid().ToString();
                
                _simulatedDrawTicketResultRepository.Insert(simulatedDrawTicketResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Save";
                string errorMethodSignature = "public static void Insert(_simulatedDrawTicketResultRepository theSimulatedDrawTicketResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        /*
*/
    }
}