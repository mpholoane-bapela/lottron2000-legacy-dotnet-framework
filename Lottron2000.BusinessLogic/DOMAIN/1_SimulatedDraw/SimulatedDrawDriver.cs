using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottron2000.Models;
using Lottron2000.Data;
using Alexis.Ydin;
using Lottron2000.Ydin;
using Lottron2000.Ydin.Utilities;

namespace Lottron2000.BusinessLogic
{
    public static class SimulatedDrawDriver
    {
        #region VARIBALES ERROR HANDLING
        private static string ERROR_OCCURED_IN_NAME_SPACE = "Lottron2000.BusinessLogic";
        private static string ERROR_OCCURED_IN_CLASS = "SimulatedDrawDriver";
        private static string ERROR_OCCURED_ON_PAGE = AlexisConstants.WebUI.DropDownSelection.CommonSelectionItems.NA.ToString();
        private static string DEFAULT_ERROR_CATEGORY_ID = AlexisConstants.ErrorLogging.ErrorCategoryIDs.System.SystemBAL.ToString();
        #endregion

        private static ILog _logger = DependecyStuffInitializor.ILogger.GetDefault();

        public static void PlayTheLottery(string playingSessionID, SimulatedDrawParameters simulatedDrawParams, List<LotteryNumbers> userProvidedPlayingTickets = null, DrawWinningNumbersCollection userProvidedWinningWinningNumbers = null)
        {
            try
            {
                // Create a draw
                var simulatedDrawNumbers = CreateDraw(playingSessionID, simulatedDrawParams, userProvidedPlayingTickets, userProvidedWinningWinningNumbers);

                // Play
                var ticketMatches = MatchTicketsToWinningNumbers(simulatedDrawNumbers, simulatedDrawParams.DrawSubCategory);

                // Store results
                StoreDrawResults(simulatedDrawNumbers.SimulatedDrawID, ticketMatches);
            }

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "PlayTheLottery";
                string errorMethodSignature = "public static void PlayTheLottery(string playingSessionID, SimulatedDrawParameters simulatedDrawParams)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }

        private static SimulatedDrawGeneratedNumbers CreateDraw(string playingSessionID, SimulatedDrawParameters simulatedDrawParams, List<LotteryNumbers> userProvidedPlayingTickets = null, DrawWinningNumbersCollection userProvidedWinningWinningNumbers = null)
        {
            try
            {
                #region CREATE SIMULATED DRAW
                SimulatedDraw theSimulatedDraw = new SimulatedDraw();
                theSimulatedDraw.PlayingNumbersGenerationMethod = simulatedDrawParams.PlayingTickets.GenerationMethod.ToString();
                theSimulatedDraw.SimulationType = simulatedDrawParams.SimulationType.ToString();
                theSimulatedDraw.WinningNumbersGenerationMethod = simulatedDrawParams.WinningNumbers.GenerationMethod.ToString();
                var createdSimultedDraw = SimulatedDrawBL.Create(playingSessionID, theSimulatedDraw);
                #endregion


                #region Create Tickets
                List<LotteryNumbers> playingTickets = new List<LotteryNumbers>();
                switch (simulatedDrawParams.PlayingTickets.GenerationMethod)
                {
                    case LottronConstants.PlayingSession.NumbersGenerationMethod.CompletelyRandom:
                        playingTickets = LotteryNumbersGenerator.PlayingTickets.GenerateRandomNumberSets(simulatedDrawParams);
                        break;

                    case LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom:
                        playingTickets = LotteryNumbersGenerator.PlayingTickets.GetRandomHistoricalWinningNumberSets(simulatedDrawParams);
                        break;

                    case LottronConstants.PlayingSession.NumbersGenerationMethod.ParameterizedRandom:
                        playingTickets = LotteryNumbersGenerator.PlayingTickets.GenerateRandomParamatizedNumberSets(simulatedDrawParams);
                        break;

                    case LottronConstants.PlayingSession.NumbersGenerationMethod.UserProvided:
                        playingTickets = userProvidedPlayingTickets;
                        break;
                }// END: switch

                SimulatedDrawTicketBL.SaveCollection(createdSimultedDraw.SimulatedDrawID, playingTickets);
                #endregion


                #region Create Winning Numbers
                //Create Winning Num Sets (only 2 - a)MainLotto b) LottoPlus)
                DrawWinningNumbersCollection winningNumbers = new DrawWinningNumbersCollection();
                switch (simulatedDrawParams.WinningNumbers.GenerationMethod)
                {
                    case LottronConstants.PlayingSession.NumbersGenerationMethod.CompletelyRandom:
                        winningNumbers = LotteryNumbersGenerator.WinningNumbers.GenerateRandonNumberSet(simulatedDrawParams);
                        break;

                    case LottronConstants.PlayingSession.NumbersGenerationMethod.ParameterizedRandom:
                        winningNumbers = LotteryNumbersGenerator.WinningNumbers.GenerateParmatizedRandonNumberSet(simulatedDrawParams);
                        break;

                    case LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom:
                        winningNumbers = LotteryNumbersGenerator.WinningNumbers.GetRandomHistoricalWinningNumberSets(simulatedDrawParams);
                        break;

                    case LottronConstants.PlayingSession.NumbersGenerationMethod.UserProvided:
                        winningNumbers = userProvidedWinningWinningNumbers;
                        break;
                }
                SimulatedDrawWinningNumberBL.SaveCollection(createdSimultedDraw.SimulatedDrawID, winningNumbers);
                #endregion


                #region Create Winning Prizes
                DrawWinningPrizeSet winningPrizeSet = MockWinningPrizeGenerator.Payout.GenerateHistoricalRandom(simulatedDrawParams.WinningPrizes).FirstOrDefault();

                // Save to db
                SimulatedDrawPrizeBL.SaveDrawWinningPrizeSet(createdSimultedDraw.SimulatedDrawID, winningPrizeSet);

                #endregion

                SimulatedDrawGeneratedNumbers simulatedDrawGeneratedNumbers = new SimulatedDrawGeneratedNumbers(playingTickets, winningNumbers, winningPrizeSet);
                simulatedDrawGeneratedNumbers.SimulatedDrawID = createdSimultedDraw.SimulatedDrawID;

                return simulatedDrawGeneratedNumbers;
            }

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "CreateDraw";
                string errorMethodSignature = "private static void CreateDraw(string playingSessionID, SimulatedDrawParameters simulatedDrawParams)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                return null;
            }
            #endregion
        }

        private static List<DrawTicketMatch> MatchTicketsToWinningNumbers(SimulatedDrawGeneratedNumbers generatedNumbers, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
        {
            try
            {
                var mainLottoNumbers = generatedNumbers.WinningNumbers.MainLottoNumbersCollection.FirstOrDefault();
                var lottoPlusNumbers = generatedNumbers.WinningNumbers.LottoPlusNumbersCollection.FirstOrDefault();
                var playingTickets = generatedNumbers.PlayingTickets;

                var mainLottoPrizeAndWinner = generatedNumbers.DrawWinningPrizeSet.MainLottoPrizeAndWinner;
                var lottoPlusPrizeAndWinner = generatedNumbers.DrawWinningPrizeSet.LottoPlusPrizeAndWinner;

                List<DrawTicketMatch> ticketMatches = new List<DrawTicketMatch>();

                foreach (var ticket in playingTickets)
                {
                    //  Match Main Lottery
                    var mainLotteryMatch = TicketToWinningMatcher.MatchSingleTicketToWinningNumbers(LottronConstants.PlayingSession.DrawSubCategory.MainLotto, ticket, mainLottoNumbers, mainLottoPrizeAndWinner);
                    ticketMatches.Add(mainLotteryMatch);

                    //  Lotto Plus Number
                    if (drawSubCategory == LottronConstants.PlayingSession.DrawSubCategory.LottoPlus)
                    {
                        var lottoPlusMatch = TicketToWinningMatcher.MatchSingleTicketToWinningNumbers(LottronConstants.PlayingSession.DrawSubCategory.LottoPlus, ticket, lottoPlusNumbers, lottoPlusPrizeAndWinner);
                        ticketMatches.Add(lottoPlusMatch);
                    }
                }
                return ticketMatches;
            }

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "MatchPlayingAndWinningNumbers";
                string errorMethodSignature = "private static void MatchPlayingAndWinningNumbers(SimulatedDrawGeneratedNumbers generatedNumbers)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
                return null;
            }
            #endregion
        }

        private static void StoreDrawResults(string simultedDrawID, List<DrawTicketMatch> drawTicketMatches)
        {
            try
            {
                DrawTicketSetMatch drawTicketMatch = new DrawTicketSetMatch();
                foreach (var drawTicket in drawTicketMatches)
                {
                    SimulatedDrawTicketResultBL.SaveDrawTicketMatchToDb(drawTicket);
                    drawTicketMatch.IncrementStats(drawTicket);
                }

                SimulatedDrawResultBL.SaveDrawTicketSet(simultedDrawID, drawTicketMatch);

            }

            #region CATCH EXCEPTION
            catch (Exception ex)
            {
                string errorMethod = "StoreDrawResults";
                string errorMethodSignature = "private static void StoreDrawResults(List<DrawTicketMatch> drawTicketMatches)";
                string ERROR_OCCURED_IN_METHOD = errorMethod + ", " + errorMethodSignature;
                _logger.LogError(DEFAULT_ERROR_CATEGORY_ID, ERROR_OCCURED_ON_PAGE, ERROR_OCCURED_IN_NAME_SPACE, ERROR_OCCURED_IN_CLASS, ERROR_OCCURED_IN_METHOD, ex);
            }
            #endregion
        }
    }
}