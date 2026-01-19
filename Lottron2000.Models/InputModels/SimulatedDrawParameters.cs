using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottron2000.Ydin;

namespace Lottron2000.Models
{
    public class SimulatedDrawParameters
    {
        public LottronConstants.PlayingSession.DrawSimulationType SimulationType { get; set; }
        public LottronConstants.PlayingSession.DrawSubCategory DrawSubCategory { get; set; }

        public PlayingTickets PlayingTickets { get; set; }
        public WinningNumbers WinningNumbers { get; set; }
        public WinningPrizes WinningPrizes { get; set; }

        public SimulatedDrawParameters()
        {
            PlayingTickets = new PlayingTickets();
            WinningNumbers = new WinningNumbers();
            WinningPrizes = new Models.WinningPrizes();
            DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus;
        }

        public SimulatedDrawParameters(PlayingTickets playingTickets, WinningNumbers winningNumbers,WinningPrizes winningPrizes)
        {
            PlayingTickets = playingTickets;
            WinningNumbers = winningNumbers;
            DrawSubCategory = LottronConstants.PlayingSession.DrawSubCategory.LottoPlus;
            WinningPrizes = winningPrizes;
        }

        public SimulatedDrawParameters(PlayingTickets playingTickets, WinningNumbers winningNumbers, WinningPrizes winningPrizes, LottronConstants.PlayingSession.DrawSubCategory drawSubCategory)
        {
            PlayingTickets = playingTickets;
            WinningNumbers = winningNumbers;
            DrawSubCategory = drawSubCategory;
            WinningPrizes = winningPrizes;
        }
    }

    //
    public class WinningNumbers
    {
        public int SetsQuantity { get; set; }
        public LottronConstants.PlayingSession.NumbersGenerationMethod GenerationMethod { get; set; }
        public DateTime? HistoricalFrom { get; set; }
        public DateTime? HistoricalTo { get; set; }

        public int MinCheckSumCount { get; set; }
        public int MaxCheckSumCount { get; set; }

        public int MinCheckSum { get; set; }
        public int MaxCheckSum { get; set; }


        public WinningNumbers()
        {
            SetsQuantity = 1;
            GenerationMethod = LottronConstants.PlayingSession.NumbersGenerationMethod.CompletelyRandom;
            HistoricalFrom = null;
            HistoricalTo = null;

            MinCheckSum = MinCheckSumCount = LottronConstants.PlayingSession.CheckSumMin;
            MaxCheckSum = MaxCheckSumCount = LottronConstants.PlayingSession.CheckSumMax;
        }

        public WinningNumbers(LottronConstants.PlayingSession.NumbersGenerationMethod numbersGenerationMethod)
        {
            SetsQuantity = 1;
            GenerationMethod = numbersGenerationMethod;
            HistoricalFrom = null;
            HistoricalTo = null;

            MinCheckSum = MinCheckSumCount = LottronConstants.PlayingSession.CheckSumMin;
            MaxCheckSum = MaxCheckSumCount = LottronConstants.PlayingSession.CheckSumMax;
        }

        public WinningNumbers(LottronConstants.PlayingSession.NumbersGenerationMethod numbersGenerationMethod, int minCheckSumCount, int maxCheckSumCount, DateTime? historicalFrom=null, DateTime? historicalTo=null)
        {
            SetsQuantity = 1;
            GenerationMethod = numbersGenerationMethod;

            MinCheckSumCount = minCheckSumCount;
            MaxCheckSumCount = maxCheckSumCount;

            if (historicalFrom.HasValue)
            {
                HistoricalFrom = historicalFrom;
            }

            if(historicalTo.HasValue)
            {
                HistoricalTo = historicalTo;
            }
        }
    }

    public class PlayingTickets
    {
        public int Quantity { get; set; }
        public decimal Budget { get; set; }
        public LottronConstants.PlayingSession.NumbersGenerationMethod GenerationMethod { get; set; }

        public DateTime? HistoricalFrom { get; set; }
        public DateTime? HistoricalTo { get; set; }

        public int MinCheckSumCount { get; set; }
        public int MaxCheckSumCount { get; set; }

        public int MinCheckSum { get; set; }
        public int MaxCheckSum { get; set; }

        public PlayingTickets()
        {
            Quantity = 1;
            Budget = 0;
            GenerationMethod = LottronConstants.PlayingSession.NumbersGenerationMethod.CompletelyRandom;
            HistoricalFrom = null;
            HistoricalTo = null;

            MinCheckSumCount = LottronConstants.PlayingSession.CheckSumMin;
            MaxCheckSumCount = LottronConstants.PlayingSession.CheckSumMax;
        }

        public PlayingTickets(int quantity, decimal budget, LottronConstants.PlayingSession.NumbersGenerationMethod generationMethod)
        {
            Quantity = quantity;
            Budget = budget;
            GenerationMethod = generationMethod;

            HistoricalFrom = null;
            HistoricalTo = null;

            MinCheckSumCount = LottronConstants.PlayingSession.CheckSumMin;
            MaxCheckSumCount = LottronConstants.PlayingSession.CheckSumMax;
        }
    }

    public class WinningPrizes
    {
        public int SetsQuantity { get; set; }
        public LottronConstants.PlayingSession.NumbersGenerationMethod PrizeGenerationMethod { get; set; }
        public LottronConstants.PlayingSession.NumbersGenerationMethod PrizeGenerationShareMethod { get; set; }

        public WinningPrizes()
        {
            SetsQuantity = 1;
            PrizeGenerationMethod = LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom;
            PrizeGenerationShareMethod = LottronConstants.PlayingSession.NumbersGenerationMethod.HistoricalRandom;
        }

        public WinningPrizes(int setsQuantity, LottronConstants.PlayingSession.NumbersGenerationMethod prizeGenerationMethod, LottronConstants.PlayingSession.NumbersGenerationMethod prizeShareGenerationMethod)
        {
            SetsQuantity = setsQuantity;
            PrizeGenerationMethod = prizeGenerationMethod;
            PrizeGenerationShareMethod = prizeShareGenerationMethod;
        }
    }
}