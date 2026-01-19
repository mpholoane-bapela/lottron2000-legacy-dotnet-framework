using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Ydin
{
    public partial class LottronConstants
    {
        public static class PlayingSession
        {
            public static int CheckSumMin = 6;
            public static int CheckSumMax = 294;

            public enum SessionSimulationType
            {
                Uniform,
                Misc
            }
            
            public enum DrawSimulationType
            {
                LiveRandomWinningNumbers,
                LiveHistoricWinningNumbers,
                TimeBasedRandomWinningNumbers,
                TimeBasedHistoricWinningNumbers
            }
            
            public enum DrawSubCategory
            {
                MainLotto,
                LottoPlus
            }

            public enum NumbersGenerationMethod
            {
                CompletelyRandom,
                ParameterizedRandom,
                HistoricalRandom,
                UserProvided,
            }

            #region DATE TIME
            public static DateTime DateTimeDinousourAge = new DateTime(1900, 1, 1);
            public static DateTime DateTimeHologramAge = new DateTime(3000, 1, 1);
            
            #endregion

        }
    }
}
