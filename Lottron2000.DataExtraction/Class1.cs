using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lottron2000.Data;


namespace Lottron2000.DataExtraction
{
    public static class WinningNumbersCreator
    {

        public static void InsertAllNumbersIntoDb()
        {
            string fileName = "1-49-001.txt";

            string fileIndex = "";
            for (int i = 1; i < 281; i++)
            {
                if (i < 10)
                {                    fileIndex = "00" + i.ToString();                }

                else if (i < 100)
                { fileIndex = "0" + i.ToString(); }

                else
                { fileIndex = i.ToString(); }

                fileName = String.Format("1-49-{0}.txt", fileIndex);
                ReadAFile(fileName);
            }
        
        }

        private static void ReadAFile(string fileName)
        {
            const string fileLocation = @"C:\aPROJECTS\TEST_PLAY\Lottron2000\Lottron2000.DataExtraction\Data\PossibleNumbers\";
            string path = fileLocation + fileName;


            //var fileContent = File.ReadAllText(path);
            //var array = fileContent.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
            //var numbers = array.Select(arg => int.Parse(arg)).ToList();


            int skipNumber = 0;

            int[][] array = File.ReadAllLines(path).Skip(skipNumber).Select(line => line.Trim().Split().Select(s => int.Parse(s)).ToArray()).ToArray();


            WinningPermutation_EntityFrameworkRepository repo = new WinningPermutation_EntityFrameworkRepository();
      
            for (int i = 0; i < array.GetLength(0); i++)
            {
                WinningPermutation winninPermutation = new WinningPermutation();
                winninPermutation.CheckSum = 0;
                winninPermutation.Created = DateTime.Now;

                for (int j = 0; j < 6; j++)
                {
                    switch (j)
                    {
                        case 0: winninPermutation.Number1 = array[i][j]; winninPermutation.CheckSum = winninPermutation.CheckSum + array[i][j]; break;
                        case 1: winninPermutation.Number2 = array[i][j]; winninPermutation.CheckSum = winninPermutation.CheckSum + array[i][j]; break;
                        case 2: winninPermutation.Number3 = array[i][j]; winninPermutation.CheckSum = winninPermutation.CheckSum + array[i][j]; break;
                        case 3: winninPermutation.Number4 = array[i][j]; winninPermutation.CheckSum = winninPermutation.CheckSum + array[i][j]; break;
                        case 4: winninPermutation.Number5 = array[i][j]; winninPermutation.CheckSum = winninPermutation.CheckSum + array[i][j]; break;
                        case 5: winninPermutation.Number6 = array[i][j]; winninPermutation.CheckSum = winninPermutation.CheckSum + array[i][j]; break;
                    }
                }

                repo.Insert(winninPermutation);
            
            }
            
            int x = 0;
        }
    }
}