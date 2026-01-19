using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Data;
using Alexis.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class PlayingSessionBL
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "PlayingSessionBL";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        #region INITIALIZATION
        private static IPlayingSessionRepository _playingSessionRepository;
        private static ILog _logger;

        public static void Init(IPlayingSessionRepository playingSessionRepository, ILog logger)
        {
            _playingSessionRepository = playingSessionRepository;
            _logger = logger;
        }
        #endregion

        #region COMMON QUERIES
        public static IQueryable<PlayingSession> GetAll()
        {
            return _playingSessionRepository.GetAll();
        }

        public static PlayingSession GetByID(string PlayingSessionID)
        {
            return _playingSessionRepository.GetByID(PlayingSessionID);
        }

        public static PlayingSession GetByItemID(int itemID)
        {
            return _playingSessionRepository.GetByItemID(itemID);
        }

        public static void Insert(PlayingSession thePlayingSession)
        {
            #region IMPLEMENTATION
            try
            {
                _playingSessionRepository.Insert(thePlayingSession);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Insert";
                string errorMethodSignature = "public static void Insert(_playingSessionRepository thePlayingSession)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void Delete(PlayingSession thePlayingSession)
        {
            #region IMPLEMENTATION
            try
            {
                _playingSessionRepository.Delete(thePlayingSession);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "Delete";
                string errorMethodSignature = "public static void Delete(_playingSessionRepository thePlayingSession)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        public static void DeleteByID(string PlayingSessionID)
        {
            #region IMPLEMENTATION
            try
            {
                _playingSessionRepository.DeleteByID(PlayingSessionID);
            }
            #endregion

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "DeleteByID";
                string errorMethodSignature = "public static void DeleteByID(string PlayingSessionID)";
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