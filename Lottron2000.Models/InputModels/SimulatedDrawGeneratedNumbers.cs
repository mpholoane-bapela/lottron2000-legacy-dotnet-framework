using System;
using System.Collections.Generic;
using System.Linq;
using Lottron2000.Ydin;

namespace Lottron2000.Models
{
    public class SimulatedDrawGeneratedNumbers
    {
        public string SimulatedDrawID { get; set; }
        public List<LotteryNumbers> PlayingTickets { get; set; }
        public DrawWinningNumbersCollection WinningNumbers { get; set; }
        public DrawWinningPrizeSet DrawWinningPrizeSet { get; set; }

        public SimulatedDrawGeneratedNumbers()
        {
            PlayingTickets = new List<LotteryNumbers>();
            WinningNumbers = new DrawWinningNumbersCollection();
            DrawWinningPrizeSet = new DrawWinningPrizeSet();
        }

        public SimulatedDrawGeneratedNumbers(List<LotteryNumbers> playingTickets, DrawWinningNumbersCollection winningNumbers, DrawWinningPrizeSet drawWinningPrizeSet)
        {
            PlayingTickets = playingTickets;
            WinningNumbers = winningNumbers;
            DrawWinningPrizeSet = drawWinningPrizeSet;
        }
    }
}