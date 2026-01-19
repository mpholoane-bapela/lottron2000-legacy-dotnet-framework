using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Ydin;

namespace Lottron2000.Models
{
    public class WinningPrize
    {
        #region ATTRIBUTES
        LottronConstants.PlayingSession.DrawSubCategory DrawSubCategory { get; set; }
        public int Div1 { get; set; }
        public int Div2 { get; set; }
        public int Div3 { get; set; }
        public int Div4 { get; set; }
        public int Div5 { get; set; }
        public int Div6 { get; set; }
        public int Div7 { get; set; }
        #endregion

        public WinningPrize()
        {
        }

        public WinningPrize(int div1,int div2,int div3,int div4,int div5,int div6, int div7,LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
        {
            Div1 = div1;
            Div2 = div2;
            Div3 = div3;
            Div4 = div4;
            Div5 = div5;
            Div6 = div6;
            Div7 = div7;
            DrawSubCategory = drawSubCategory;
        }
    }
}
