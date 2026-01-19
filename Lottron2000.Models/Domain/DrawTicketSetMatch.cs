using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Models
{
    // The global sum of all ticket matches
    public class DrawTicketSetMatch
    {
        #region ATTRIBUTES
        public int AmountWon { get; set; }
        public int AmountSpent { get; set; }
        public int AmountLost { get; set; }
        public int Num1Matches { get; set; }
        public int Num2Matches { get; set; }
        public int Num3Matches { get; set; }
        public int Num4Matches { get; set; }
        public int Num5Matches { get; set; }
        public int Num6Matches { get; set; }
        public int BonusMatches { get; set; }
        #endregion

        public DrawTicketSetMatch()
        {
            AmountWon = 0;
            AmountSpent = 0;
            AmountLost = 0;

            Num1Matches = 0;
            Num2Matches = 0;
            Num3Matches = 0;
            Num4Matches = 0;
            Num5Matches = 0;
            Num6Matches = 0;
            BonusMatches = 0;
        }

        public DrawTicketSetMatch(int amountWon, int amountSpent, int amountLost, int num1Matches, int num2Matches, int num3Matches, int num4Matches, int num5Matches, int num6Matches, int bonusMatches)
        {
            AmountWon = amountWon;
            AmountSpent = amountSpent;
            AmountLost = amountLost;
            Num1Matches = num1Matches;
            Num2Matches = num2Matches;
            Num3Matches = num3Matches;
            Num4Matches = num4Matches;
            Num5Matches = num5Matches;
            Num6Matches = num6Matches;
            BonusMatches = bonusMatches;
        }

        public void IncrementStats(DrawTicketMatch drawTicketMatch)
        {
            #region MATCH EACH NUMBER
            switch (drawTicketMatch.NumbersMatched)
            { 
                case 1:
                    Num1Matches++;
                    break;

                case 2:
                    Num2Matches++;
                    break;

                case 3:
                    Num3Matches++;
                    break;

                case 4:
                    Num4Matches++;
                    break;

                case 5:
                    Num5Matches++;
                    break;

                case 6:
                    Num6Matches++;
                    break;
            }
            #endregion

            if (drawTicketMatch.BonusBallMatch)
            {
                BonusMatches++;
            }
        }
    }
}