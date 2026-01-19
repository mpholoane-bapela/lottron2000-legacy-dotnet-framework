using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Models;
using AutoMapper;
using Lottron2000.BusinessLogic.BAL.AutoMappers;

namespace Lottron2000.BusinessLogic
{
    public static class SALottoPlusResultBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SALottoPlusResultBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISALottoPlusResultRepository _sALottoPlusResultRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(ISALottoPlusResultRepository sALottoPlusResultRepository, ILog logger)
        {
            _sALottoPlusResultRepository = sALottoPlusResultRepository;
            _logger = logger;

            _mapper = Mapper.Engine;
            SALottoPlusResults_AutoMapperConfig.CreateMap();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SALottoPlusResult> GetAll()
        {
            return _sALottoPlusResultRepository.GetAll();
        }

        public static SALottoPlusResult GetByID(string SALottoPlusResultID)
        {
            return _sALottoPlusResultRepository.GetByID(SALottoPlusResultID);
        }

        public static SALottoPlusResult GetByItemID(int itemID)
        {
            return _sALottoPlusResultRepository.GetByItemID(itemID);
        }

        public static IQueryable<SALottoPlusResult> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _sALottoPlusResultRepository.GetByDateRange(fromDate, toDate);
        }

        public static IQueryable<SALottoPlusResult> GetByRange(DateTime fromDate, DateTime toDate, int minCheckSum, int maxCheckSum)
        {
            return _sALottoPlusResultRepository.GetByRange(fromDate, toDate, minCheckSum, maxCheckSum);
        }

        public static void Insert(SALottoPlusResult theSALottoPlusResult)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoPlusResultRepository.Insert(theSALottoPlusResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_sALottoPlusResultRepository theSALottoPlusResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Update(SALottoPlusResult theSALottoPlusResult)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoPlusResultRepository.Update(theSALottoPlusResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Update";
                string errorMethodSignature = "public static void Update(SALottoPlusResult theSALottoPlusResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SALottoPlusResult theSALottoPlusResult)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoPlusResultRepository.Delete(theSALottoPlusResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_sALottoPlusResultRepository theSALottoPlusResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string SALottoPlusResultID)
        {
            #region IMPLEMENTATION
            try
            {
                //_sALottoPlusResultRepository.DeleteByID(SALottoPlusResultID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string SALottoPlusResultID)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        #endregion


        public static LotteryNumbers MapToLotteryNumbers(SALottoPlusResult lottoPlusResult)
        {
            return _mapper.Map<SALottoPlusResult, LotteryNumbers>(lottoPlusResult);
        }

        public static IEnumerable<LotteryNumbers> MapToLotteryNumbers(IQueryable<SALottoPlusResult> lottoPlusResult)
        {
            return _mapper.Map<IQueryable<SALottoPlusResult>, IEnumerable<LotteryNumbers>>(lottoPlusResult);
        }

        /*
*/
    }
}