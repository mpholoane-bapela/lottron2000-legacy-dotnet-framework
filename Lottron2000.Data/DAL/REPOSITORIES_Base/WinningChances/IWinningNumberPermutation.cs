using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexis.Ydin;

namespace Lottron2000.Data
{
    public interface IWinningNumberPermutationRepository : IRepository<WinningNumberPermutation>
    {
        IQueryable<WinningNumberPermutation> GetByRange(int minCheckSum, int maxCheckSum);
        int CountGetByRange(int minCheckSum, int maxCheckSum);
    }
}