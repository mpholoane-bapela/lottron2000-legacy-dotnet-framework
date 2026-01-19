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
    public static class MockWinningPrizeShareBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "MockWinningPrizeShareBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IMockWinningPrizeShareRepository _mockWinningPrizeShareRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(IMockWinningPrizeShareRepository mockWinningPrizeShareRepository, ILog logger)
        {
            _logger = logger;
            _mockWinningPrizeShareRepository = mockWinningPrizeShareRepository;

            _mapper = Mapper.Engine;
            MockWinningPrizeShare_AutoMapperConfig.CreateMap();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<MockWinningPrizeShare> GetAll()
        {
            return _mockWinningPrizeShareRepository.GetAll();
        }

        public static MockWinningPrizeShare GetByID(string MockWinningPrizeShareID)
        {
            return _mockWinningPrizeShareRepository.GetByID(MockWinningPrizeShareID);
        }

        public static MockWinningPrizeShare GetByItemID(int itemID)
        {
            return _mockWinningPrizeShareRepository.GetByItemID(itemID);
        }

        public static void Insert(MockWinningPrizeShare theMockWinningPrizeShare)
        {
            #region IMPLEMENTATION
            try
            {
                _mockWinningPrizeShareRepository.Insert(theMockWinningPrizeShare);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_mockWinningPrizeShareRepository theMockWinningPrizeShare)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(MockWinningPrizeShare theMockWinningPrizeShare)
        {
            #region IMPLEMENTATION
            try
            {
                _mockWinningPrizeShareRepository.Delete(theMockWinningPrizeShare);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_mockWinningPrizeShareRepository theMockWinningPrizeShare)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string MockWinningPrizeShareID)
        {
            #region IMPLEMENTATION
            try
            {
                //_mockWinningPrizeShareRepository.DeleteByID(MockWinningPrizeShareID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string MockWinningPrizeShareID)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static WinningPrizeWinner MapToWinningPrizeShare(MockWinningPrizeShare mockWinningPrizeShare)
        {
            return _mapper.Map<MockWinningPrizeShare, WinningPrizeWinner>(mockWinningPrizeShare);
        }

        #endregion

        /*
*/
    }
}