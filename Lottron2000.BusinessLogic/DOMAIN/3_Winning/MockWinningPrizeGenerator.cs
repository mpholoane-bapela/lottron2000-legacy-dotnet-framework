using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottron2000.BusinessLogic
{
    public static class MockWinningPrizeGenerator
    {

        public static List<LotteryNumbers> Generate(SimulatedDrawParameters simulatedDrawParams)
        {
            try
            {
                if ((int)simulatedDrawParams.PlayingTickets.Budget > 0)
                {
                    return GenerateRandomNumberSets(simulatedDrawParams.PlayingTickets.Budget, simulatedDrawParams.DrawSubCategory);
                }

                else
                {
                    return GenerateRandomNumberSets(simulatedDrawParams.PlayingTickets.Quantity, simulatedDrawParams.DrawSubCategory);
                }
            }

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "GenerateRandomNumberSets";
                string errorMethodSignature = "public static List<LotteryNumbers> GenerateRandomNumberSets(SimulatedDrawParameters simulatedDrawParams)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                return null;
            }
            #endregion
        }
    
    }
}