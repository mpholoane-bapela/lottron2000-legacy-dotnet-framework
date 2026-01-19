using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Models;
using AutoMapper;
using Lottron2000.BusinessLogic.BAL.AutoMappers;
using Lottron2000.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class SimulatedDrawWinningNumberBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawWinningNumberBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISimulatedDrawWinningNumberRepository _simulatedDrawWinningNumberRepository;
        private static ILog _logger;
        private static IMappingEngine _mapper = null;


        public static void Init(ISimulatedDrawWinningNumberRepository simulatedDrawWinningNumberRepository, ILog logger)
        {
            _logger = logger;
            _simulatedDrawWinningNumberRepository = simulatedDrawWinningNumberRepository;

            _mapper = Mapper.Engine;
            SimulatedDrawWinningNumber_AutoMapperConfig.CreateDomainToDbMapping();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SimulatedDrawWinningNumber> GetAll()
        {
            return _simulatedDrawWinningNumberRepository.GetAll();
        }

        //public static IQueryable<SimulatedDrawWinningNumber> GetByID(string SimulatedDrawWinningNumberID)
        //{
        //    return _simulatedDrawWinningNumberRepository.GetByID(SimulatedDrawWinningNumberID);
        //}

        public static SimulatedDrawWinningNumber GetByItemID(int itemID)
        {
            return _simulatedDrawWinningNumberRepository.GetByItemID(itemID);
        }

        public static void Insert(SimulatedDrawWinningNumber theSimulatedDrawWinningNumber)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawWinningNumberRepository.Insert(theSimulatedDrawWinningNumber);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_simulatedDrawWinningNumberRepository theSimulatedDrawWinningNumber)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SimulatedDrawWinningNumber theSimulatedDrawWinningNumber)
        {
            #region IMPLEMENTATION
            try
            {
                _simulatedDrawWinningNumberRepository.Delete(theSimulatedDrawWinningNumber);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_simulatedDrawWinningNumberRepository theSimulatedDrawWinningNumber)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        //public static void DeleteByID(string SimulatedDrawWinningNumberID)
        //{
        //    #region IMPLEMENTATION
        //    try
        //    {
        //        _simulatedDrawWinningNumberRepository.DeleteByID(SimulatedDrawWinningNumberID);
        //    }
        //    #endregion

        //    #region CATCH EXCEPTION
        //    catch (Exception ex)
        //    {
        //        string errorMethod = "DeleteByID";
        //        string errorMethodSignature = "public static void DeleteByID(string SimulatedDrawWinningNumberID)";
        //        string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
        //        _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
        //    }
        //    #endregion
        //}

        #endregion

        #region OTHERS
        public static SimulatedDrawWinningNumber MapLotteryNumbersToDb(LotteryNumbers lotteryNumbers)
        {
            return _mapper.Map<LotteryNumbers, SimulatedDrawWinningNumber>(lotteryNumbers);
        }

        public static void SaveLotterNumber(string simulatedDrawID,LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, LotteryNumbers winningNumbers)
        {
            #region IMPLEMENTATION
            try
            {
                SimulatedDrawWinningNumber dbWinningNumber = MapLotteryNumbersToDb(winningNumbers);
                dbWinningNumber.Created = DateTime.Now;
                dbWinningNumber.SimulatedDrawID = simulatedDrawID;
                dbWinningNumber.DrawSubCategory = drawSubCategory.ToString();
                dbWinningNumber.SimulatedDrawWinningNumbersID = Guid.NewGuid().ToString();

                Insert(dbWinningNumber);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "SaveLotterNumber";
                string errorMethodSignature = "public static void Insert(_simulatedDrawTicketRepository theSimulatedDrawTicket)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void SaveCollection(string simulatedDrawID, DrawWinningNumbersCollection winningNumbersCollection)
        {
            #region IMPLEMENTATION
            try
            {
                // Save Main Lottery Numbers
                var mainLottoNumbers = winningNumbersCollection.MainLottoNumbersCollection.ToList();
                foreach (var lotteryNumber in mainLottoNumbers)
                {
                    SaveLotterNumber(simulatedDrawID, LottronConstants.PlayingSession.DrawSubCategory.MainLotto ,lotteryNumber);
                }

                // Save Lotto Plus Numbers 
                var lottoPlusNumbers = winningNumbersCollection.LottoPlusNumbersCollection.ToList();
                foreach (var lotteryNumber in lottoPlusNumbers)
                {
                    SaveLotterNumber(simulatedDrawID, LottronConstants.PlayingSession.DrawSubCategory.LottoPlus, lotteryNumber);
                }
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "SaveCollection";
                string errorMethodSignature = "public static void Insert(_simulatedDrawWinningNumberRepository theSimulatedDrawWinningNumber)";
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