using System;
using Lottron2000.DataExtraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottron2000.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WinningNumbersCreator.InsertAllNumbersIntoDb();
        }
    }
}
