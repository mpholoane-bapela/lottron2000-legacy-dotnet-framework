using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Models
{
    public class DrawWinningPrizeSet
    {
        public WinningPrizeAndWinner MainLottoPrizeAndWinner { get; set; }
        public WinningPrizeAndWinner LottoPlusPrizeAndWinner { get; set; }

        public DrawWinningPrizeSet()
        {
            MainLottoPrizeAndWinner = new WinningPrizeAndWinner();
            LottoPlusPrizeAndWinner = new WinningPrizeAndWinner();
        }
    }
}