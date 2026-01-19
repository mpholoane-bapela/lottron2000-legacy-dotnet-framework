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
    public static class MockWinningPrizeBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "MockWinningPrizeBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IMockWinningPrizeRepository _mockWinningPrizeRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(IMockWinningPrizeRepository mockWinningPrizeRepository, ILog logger)
        {
            _mockWinningPrizeRepository = mockWinningPrizeRepository;
            _logger = logger;

            _mapper = Mapper.Engine;
            MockWinningPrize_AutoMapperConfig.CreateMap();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<MockWinningPrize> GetAll()
        {
            return _mockWinningPrizeRepository.GetAll();
        }

        public static MockWinningPrize GetByID(string MockWinningPrizeID)
        {
            return _mockWinningPrizeRepository.GetByID(MockWinningPrizeID);
        }

        public static MockWinningPrize GetByItemID(int itemID)
        {
            return _mockWinningPrizeRepository.GetByItemID(itemID);
        }

        public static void Insert(MockWinningPrize theMockWinningPrize)
        {
            #region IMPLEMENTATION
            try
            {
                _mockWinningPrizeRepository.Insert(theMockWinningPrize);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_mockWinningPrizeRepository theMockWinningPrize)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(MockWinningPrize theMockWinningPrize)
        {
            #region IMPLEMENTATION
            try
            {
                _mockWinningPrizeRepository.Delete(theMockWinningPrize);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_mockWinningPrizeRepository theMockWinningPrize)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string MockWinningPrizeID)
        {
            #region IMPLEMENTATION
            try
            {
                //_mockWinningPrizeRepository.DeleteByID(MockWinningPrizeID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string MockWinningPrizeID)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static WinningPrize MapToWinningPrize(MockWinningPrize mockWinningPrize)
        {
            return _mapper.Map<MockWinningPrize, WinningPrize>(mockWinningPrize);
        }

        #endregion

        /*
*/
    }
}