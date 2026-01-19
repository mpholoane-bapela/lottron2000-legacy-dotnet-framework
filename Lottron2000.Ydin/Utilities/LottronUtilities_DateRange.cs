using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;


namespace Lottron2000.Ydin.Utilities
{
    public partial class LottronUtilities
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.Ydin.Utilities";
        private static string ERROR_OCCURED_IN_CLASS = "LottronUtilities";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemUtilities.ToString();
        #endregion
        
        private static ILog _logger = DependecyStuffInitializor.ILogger.GetDefault();
        
        public static class DateRange
        {
            public static Dictionary<string,DateTime> GetNormalizedRange(DateTime? fromDate, DateTime? toDate)
            {
                try
                {
                    #region IMPLEMENTATION
                    Dictionary<string, DateTime> dateRange = new Dictionary<string, DateTime>();

                    DateTime fromDateNormalized = DateTime.MinValue;
                    DateTime toDateNormalized = DateTime.MinValue;

                    // From Age
                    if (fromDate == null)
                    {
                        fromDateNormalized = LottronConstants.PlayingSession.DateTimeDinousourAge;
                    }

                    else
                    {
                        fromDateNormalized = ((DateTime)fromDate).AddDays(-1);
                    }


                    // To Age
                    if (toDate == null)
                    {
                        toDateNormalized = LottronConstants.PlayingSession.DateTimeHologramAge;
                    }

                    else
                    {
                        toDateNormalized = ((DateTime)toDate).AddDays(1);
                    }

                    dateRange.Add(LottronConstants.CollectionKeys.DICTIONARY_DATE_FROM_DATE, fromDateNormalized);
                    dateRange.Add(LottronConstants.CollectionKeys.DICTIONARY_DATE_TO_DATE, toDateNormalized);

                    return dateRange;
                    #endregion
                }

                #region CATCH EXCEPTION
                catch (Exception ex)
                {
                    string errorMethod = "GetNormalizedRange";
                    string errorMethodSignature = "public static Dictionary<string,DateTime> GetNormalizedRange(DateTime? fromDate, DateTime? toDate)";
                    string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                    _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                    return null;
                }
                #endregion
            
            
            
            }
        }
    }
}