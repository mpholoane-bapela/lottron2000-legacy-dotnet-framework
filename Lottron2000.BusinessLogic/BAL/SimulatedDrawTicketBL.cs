using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Models;
using Lottron2000.BusinessLogic.BAL.AutoMappers;
using AutoMapper;

namespace Lottron2000.BusinessLogic
{
    public static class SimulatedDrawTicketBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawTicketBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISimulatedDrawTicketRepository _simulatedDrawTicketRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(ISimulatedDrawTicketRepository simulatedDrawTicketRepository, ILog logger)
        {
            _logger = logger;
            _simulatedDrawTicketRepository = simulatedDrawTicketRepository;

            _mapper = Mapper.Engine;
            SimulatedDrawTicket_AutoMapperConfig.CreateDomainToDbMapping();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SimulatedDrawTicket> GetAll()
        {
            return _simulatedDrawTicketRepository.GetAll();
        }

        //public static SimulatedDrawTicket GetByID(string SimulatedDrawTicketID)
        //{
        //    return _simulatedDrawTicketRepository.GetByID(SimulatedDrawTicketID);
        //}

        public static SimulatedDrawTicket GetByItemID(int itemID)
        {
            return _simulatedDrawTicketRepository.GetByItemID(itemID);
        }

        public static void Insert(SimulatedDrawTicket theSimulatedDrawTicket)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawTicketRepository.Insert(theSimulatedDrawTicket);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawTicketRepository theSimulatedDrawTicket)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SimulatedDrawTicket theSimulatedDrawTicket)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawTicketRepository.Delete(theSimulatedDrawTicket);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_simulatedDrawTicketRepository theSimulatedDrawTicket)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string SimulatedDrawTicketID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _simulatedDrawTicketRepository.DeleteByID(SimulatedDrawTicketID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string SimulatedDrawTicketID)";
        //        string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
        //        _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
        //    }
        //    #endregion
        //}

        #endregion


        #region DDD
        public static SimulatedDrawTicket MapLotteryNumbersToDb(LotteryNumbers lotteryNumbers)
        {
            return _mapper.Map<LotteryNumbers, SimulatedDrawTicket>(lotteryNumbers);
        }

        public static void SaveLotterNumber(string simulatedDrawID, LotteryNumbers playingTicket)
        {
            #region IMPLEMENTATION
            try
            {
                SimulatedDrawTicket drawTicket = MapLotteryNumbersToDb(playingTicket);
                drawTicket.Created = DateTime.Now;
                drawTicket.SimulatedDrawID = simulatedDrawID;
                drawTicket.SimulatedDrawTicketID = playingTicket.TicketUniqueID;

                Insert(drawTicket);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawTicketRepository theSimulatedDrawTicket)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void SaveCollection(string simulatedDrawID, List<LotteryNumbers> playingTickets)
        {
            #region IMPLEMENTATION
            try
            {
                foreach (var ticket in playingTickets)
                {
                    SaveLotterNumber(simulatedDrawID, ticket);
                }
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawTicketRepository theSimulatedDrawTicket)";
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