using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Ydin;

namespace Lottron2000.Models
{
    public class WinningPrizeWinner
    {
        #region ATTRIBUTES
        LottronConstants.PlayingSession.DrawSubCategory DrawSubCategory { get; set; }
        public int Div1Winners { get; set; }
        public int Div2Winners { get; set; }
        public int Div3Winners { get; set; }
        public int Div4Winners { get; set; }
        public int Div5Winners { get; set; }
        public int Div6Winners { get; set; }
        public int Div7Winners { get; set; }
        #endregion

        public WinningPrizeWinner()
        {
            Div1Winners = 0;
        }

        public WinningPrizeWinner(int div1Winners, int div2Winners, int div3Winners, int div4Winners, int div5Winners, int div6Winners, int div7Winners, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
        {
            Div1Winners = div1Winners;
            Div2Winners = div2Winners;
            Div3Winners = div3Winners;
            Div4Winners = div4Winners;
            Div5Winners = div5Winners;
            Div6Winners = div6Winners;
            Div7Winners = div7Winners;
            DrawSubCategory = drawSubCategory;
        }

    }
}