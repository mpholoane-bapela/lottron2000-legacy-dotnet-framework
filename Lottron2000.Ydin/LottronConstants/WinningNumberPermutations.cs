using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Ydin
{
    public partial class LottronConstants
    {
        public static class WinningNumberPermutations
        {
            public const int TotalPermutations = 12_000_000;

            public enum WinningDivision
            {
                Jackpot,
                Match_5_bonus,
                Match_5,
                Match_4_bonus,
                Match_4,
                Match_3_bonus,
                Match_3,
                Looser
            }
        }
    }
}