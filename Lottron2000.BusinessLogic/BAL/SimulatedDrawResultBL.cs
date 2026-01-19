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
    public static class SimulatedDrawResultBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawResultBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISimulatedDrawResultRepository _simulatedDrawResultRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(ISimulatedDrawResultRepository simulatedDrawResultRepository, ILog logger)
        {
            _logger = logger;
            _simulatedDrawResultRepository = simulatedDrawResultRepository;

            _mapper = Mapper.Engine;
            SimulatedDrawResult_AutoMapperConfig.CreateDomainToDbMapping();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SimulatedDrawResult> GetAll()
        {
            return _simulatedDrawResultRepository.GetAll();
        }

        //public static IQueryable<SimulatedDrawResult> GetByID(string SimulatedDrawResultID)
        //{
        //    return _simulatedDrawResultRepository.GetByID(SimulatedDrawResultID);
        //}

        public static SimulatedDrawResult GetByItemID(int itemID)
        {
            return _simulatedDrawResultRepository.GetByItemID(itemID);
        }

        public static void Insert(SimulatedDrawResult theSimulatedDrawResult)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawResultRepository.Insert(theSimulatedDrawResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawResultRepository theSimulatedDrawResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SimulatedDrawResult theSimulatedDrawResult)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawResultRepository.Delete(theSimulatedDrawResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_simulatedDrawResultRepository theSimulatedDrawResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string SimulatedDrawResultID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _simulatedDrawResultRepository.DeleteByID(SimulatedDrawResultID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string SimulatedDrawResultID)";
        //        string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
        //        _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
        //    }
        //    #endregion
        //}

        #endregion

        public static SimulatedDrawResult MapDrawTicketMatchToDb(DrawTicketSetMatch drawTicketSetMatch)
        {
            return _mapper.Map<DrawTicketSetMatch, SimulatedDrawResult>(drawTicketSetMatch);
        }

        public static void SaveDrawTicketSet(string simulatedDrawID, DrawTicketSetMatch drawTicketSetMatch)
        {
            #region IMPLEMENTATION
            try
            {
                SimulatedDrawResult simulatedDrawResult = MapDrawTicketMatchToDb(drawTicketSetMatch);
                simulatedDrawResult.Created = DateTime.Now;
                simulatedDrawResult.SimulatedDrawResultID = Guid.NewGuid().ToString();
                simulatedDrawResult.SimulatedDrawID = simulatedDrawID;

                _simulatedDrawResultRepository.Insert(simulatedDrawResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "SaveDrawTicketSet";
                string errorMethodSignature = "public static void SaveDrawTicketSet(string simulatedDrawID, DrawTicketSetMatch drawTicketSetMatch)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }
        /*
*/
    }
}