using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Ydin;

namespace Lottron2000.Models
{
    public class DrawTicketMatch
    {
        public int NumbersMatched { get; set; }
        public bool BonusBallMatch { get; set; }
        public bool IsJackpotWinner { get; set; }
        public LottronConstants.WinningNumberPermutations.WinningDivision WinningDivision { get; set; }
        public LottronConstants.PlayingSession.DrawSubCategory DrawSubCategory { get; set; }
        public string TicketUniqueID { get; set; }

        public int AmountWon { get; set; }

        public DrawTicketMatch()
        {
        }

        public DrawTicketMatch(LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int numbersMatched, bool bonusBallMatch, LottronConstants.WinningNumberPermutations.WinningDivision winningDivision)
        {
            NumbersMatched = numbersMatched;
            BonusBallMatch = bonusBallMatch;
            WinningDivision = winningDivision;
            DrawSubCategory = drawSubCategory;
        }

        public DrawTicketMatch(string ticketUniqueID, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int numbersMatched, bool bonusBallMatch, bool isJackpotWinner, LottronConstants.WinningNumberPermutations.WinningDivision winningDivision, int amountWon)
        {
            NumbersMatched = numbersMatched;
            BonusBallMatch = bonusBallMatch;
            WinningDivision = winningDivision;
            AmountWon = amountWon;
            IsJackpotWinner = isJackpotWinner;
            DrawSubCategory = drawSubCategory;
            TicketUniqueID = ticketUniqueID;
        }
    }
}