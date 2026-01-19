using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Ydin;

namespace Lottron2000.Models
{
    public class WinningPrizeAndWinner
    {
        #region ATTRIBUTES
        public WinningPrize WinningPrize { get; set; }
        public WinningPrizeWinner WinningPrizeWinner { get; set; }
        #endregion

        public WinningPrizeAndWinner()
        {
            WinningPrize = new WinningPrize();
            WinningPrizeWinner = new WinningPrizeWinner();
        }
    }
}