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
    public static class SALottoResultBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SALottoResultBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static ISALottoResultRepository _sALottoResultRepository;
        private static ILog _logger;

        private static IMappingEngine _mapper = null;

        public static void Init(ISALottoResultRepository sALottoResultRepository, ILog logger)
        {
            _sALottoResultRepository = sALottoResultRepository;
            _logger = logger;
            _sALottoResultRepository = sALottoResultRepository;
            _mapper = Mapper.Engine;

            SALottoResults_AutoMapperConfig.CreateMap();
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<SALottoResult> GetAll()
        {
            return _sALottoResultRepository.GetAll();
        }

        public static SALottoResult GetByID(string SALottoResultID)
        {
            return _sALottoResultRepository.GetByID(SALottoResultID);
        }

        public static SALottoResult GetByItemID(int itemID)
        {
            return _sALottoResultRepository.GetByItemID(itemID);
        }

        public static IQueryable<SALottoResult> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _sALottoResultRepository.GetByDateRange(fromDate, toDate);
        }

        public static IQueryable<SALottoResult> GetByRange(DateTime fromDate, DateTime toDate, int minCheckSum, int maxCheckSum)
        {
            return _sALottoResultRepository.GetByRange(fromDate, toDate, minCheckSum, maxCheckSum);
        }

        public static void Insert(SALottoResult theSALottoResult)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoResultRepository.Insert(theSALottoResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_sALottoResultRepository theSALottoResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(SALottoResult theSALottoResult)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoResultRepository.Delete(theSALottoResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_sALottoResultRepository theSALottoResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string SALottoResultID)
        {
            #region IMPLEMENTATION
            try
            {
                //_sALottoResultRepository.DeleteByID(SALottoResultID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string SALottoResultID)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Update(SALottoResult theSALottoResult)
        {
            #region IMPLEMENTATION
            try
            {
                _sALottoResultRepository.Update(theSALottoResult);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Update";
                string errorMethodSignature = "public static void Update(SALottoResult theSALottoResult)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        #endregion

        public static LotteryNumbers MapToLotteryNumbers(SALottoResult lottoResult)
        {
            return _mapper.Map<SALottoResult, LotteryNumbers>(lottoResult);
        }

        public static IEnumerable<LotteryNumbers> MapToLotteryNumbers(IQueryable<SALottoResult> lottoResults)
        {
            return _mapper.Map<IQueryable<SALottoResult>, IEnumerable<LotteryNumbers>>(lottoResults);
        }
        /*
*/
    }
}