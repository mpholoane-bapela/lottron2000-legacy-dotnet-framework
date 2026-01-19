using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Models
{
    public class DrawWinningNumbers
    {
        public LotteryNumbers MainLottoNumbers { get; set; }
        public LotteryNumbers LottoPlusNumbers { get; set; }
    }

    public class DrawWinningNumbersCollection
    {
        public IEnumerable<LotteryNumbers> MainLottoNumbersCollection { get; set; }
        public IEnumerable<LotteryNumbers> LottoPlusNumbersCollection { get; set; }

        public DrawWinningNumbersCollection()
        {
            MainLottoNumbersCollection = null;
            LottoPlusNumbersCollection = null;
        }

        public DrawWinningNumbersCollection(IEnumerable<LotteryNumbers> mainLottoNumbersCollection, IEnumerable<LotteryNumbers> lottoPlusNumbersCollection)
        {
            MainLottoNumbersCollection = mainLottoNumbersCollection;
            LottoPlusNumbersCollection = lottoPlusNumbersCollection;
        }

    }
}