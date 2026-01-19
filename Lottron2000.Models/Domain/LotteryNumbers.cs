using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottron2000.Models
{
    public class LotteryNumbers
    {
        public int Number1 {get;set;}
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Bonus { get; set; }
        public int CheckSum { get; set; }
        public int CheckSumCount { get; set; }

        public string TicketUniqueID { get; set; }

        public LotteryNumbers()
        { }

        public LotteryNumbers(string uniqueID,int number1, int number2, int number3, int number4, int number5, int number6, int bonus)
        {
            Number1 = number1;
            Number2 = number2;
            Number3 = number3;
            Number4 = number4;
            Number5 = number5;
            Number6 = number6;
            Bonus = bonus;

            CheckSum = number1 + number2 + number3 + number4 + number5 + number6;
            CheckSumCount = 0;
            TicketUniqueID = uniqueID;
        }

        public LotteryNumbers(string uniqueID,int? number1, int? number2, int? number3, int? number4, int? number5, int? number6, int? bonus)
        {
            Number1 =(int) number1;
            Number2 = (int)number2;
            Number3 = (int)number3;
            Number4 = (int)number4;
            Number5 = (int)number5;
            Number6 = (int)number6;
            
            if (bonus == null)
            { Bonus = 0; }

            else
            { Bonus = (int)bonus; }


            CheckSum = Number1 + Number2 + Number3 + Number4 + Number5 + Number6;
            CheckSumCount = 0;

            TicketUniqueID = uniqueID;
        }


        public List<int> Get6NumbersAsList()
        {
            List<int> listOfNumbers = new List<int>() { Number1, Number2, Number3, Number4, Number5, Number6 };
            listOfNumbers.Sort();
            return listOfNumbers;
        }
    
    }
}