using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexis.Ydin;
using Lottron2000.Models;
using Lottron2000.Ydin;

namespace Lottron2000.BusinessLogic
{
    public static class TicketToWinningMatcher
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "TicketToWinningMatcher";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        private static ILog _logger;
        public static void Init(ILog logger)
        {
            _logger = logger;
        }

        public static DrawTicketMatch MatchSingleTicketToWinningNumbers(LottronConstants.PlayingSession.DrawSubCategory drawSubCategory,LotteryNumbers playingTicket, LotteryNumbers targetNumbers,WinningPrizeAndWinner winningPrizeAndWinner)
        {
            try
            {
                var playingTicket6Digits = playingTicket.Get6NumbersAsList();
                var target6Digits = targetNumbers.Get6NumbersAsList();
                //var lottoPlus6Digits = lottoPlusNumbers.Get6NumbersAsList();

                int numMatches = 0;
                bool bonusMatch = false;

                var matchingDigits = playingTicket6Digits.Intersect(target6Digits).ToList();
                numMatches = matchingDigits.Count;

                // Bonus ball is considered
                if (numMatches < 6)
                {
                    var seedForBonusBall = matchingDigits;
                    seedForBonusBall.Remove(targetNumbers.Bonus);

                    foreach (var digit in seedForBonusBall)
                    {
                        if (digit == targetNumbers.Bonus)
                        {
                            bonusMatch = true;
                        }
                    }
                }

                var ticketMatch = GetWinningDivision(playingTicket.TicketUniqueID, drawSubCategory, numMatches, bonusMatch, winningPrizeAndWinner);
                return ticketMatch;
            }

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "MatchSingleTicketToWinningNumbers";
                string errorMethodSignature = "public static DrawTicketMatch MatchSingleTicketToWinningNumbers(LotteryNumbers playingTicket, LotteryNumbers targetNumbers,WinningPrizeAndWinner winningPrizeAndWinner)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                return null;
            }
            #endregion
        }

        public static DrawTicketMatch GetWinningDivision(string ticketUniqueID,LottronConstants.PlayingSession.DrawSubCategory drawSubCategory, int numberOfMatches, bool bonusMatch, WinningPrizeAndWinner winningPrizeAndWinner)
        {
            try
            {
                LottronConstants.WinningNumberPermutations.WinningDivision winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Looser;
                int amountWon = 0;
                bool isJackpotWinner = false;

                switch (numberOfMatches)
                {
                    case 6:
                        winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Jackpot;
                        amountWon = winningPrizeAndWinner.WinningPrize.Div1;
                        isJackpotWinner = true;
                        break;

                    case 5:
                        winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Match_5;
                        amountWon = winningPrizeAndWinner.WinningPrize.Div3;

                        if (bonusMatch)
                        {
                            winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Match_5_bonus;
                            amountWon = winningPrizeAndWinner.WinningPrize.Div2;
                        }
                        break;

                    case 4:
                        winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Match_4;
                        amountWon = winningPrizeAndWinner.WinningPrize.Div5;

                        if (bonusMatch)
                        {
                            winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Match_4_bonus;
                            amountWon = winningPrizeAndWinner.WinningPrize.Div4;
                        }
                        break;

                    case 3:
                        winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Match_3;
                        amountWon = winningPrizeAndWinner.WinningPrize.Div7;

                        if (bonusMatch)
                        {
                            winningDivision = LottronConstants.WinningNumberPermutations.WinningDivision.Match_3_bonus;
                            amountWon = winningPrizeAndWinner.WinningPrize.Div6;
                        }
                        break;
                }

                DrawTicketMatch ticketMatch = new DrawTicketMatch(ticketUniqueID, drawSubCategory, numberOfMatches, bonusMatch, isJackpotWinner, winningDivision, amountWon);

                return ticketMatch;
            }

            catch (Exception ex)
            {
                string errorMethod = "GetWinningDivision";
                string errorMethodSignature = "public static DrawTicketMatch GetWinningDivision(int numberOfMatches, bool bonusMatch, WinningPrizeAndWinner winningPrizeAndWinner)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                return null;
            }
        }
    }
}