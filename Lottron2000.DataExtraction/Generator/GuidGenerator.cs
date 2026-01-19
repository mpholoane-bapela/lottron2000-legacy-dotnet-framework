using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexis.Ydin;

namespace Lottron2000.DataExtraction
{
    public static class GuidGenerator
    {

        public static void GenerateAndSaveToTextFile(int quantity,string fileLocation)
        {
            var guidsList = GenerateGuids(quantity);
            SaveToTextFile(guidsList, fileLocation);
        }

        public static List<string> GenerateGuids(int quantity)
        {
            List<string> guidsList = new List<string>();
            for (int i = 0; i < quantity; i++)
            {
                guidsList.Add(Guid.NewGuid().ToString());
            }
            return guidsList;
        }

        private static void SaveToTextFile(List<string> guidsList,string fileLocation)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(var guid in guidsList)
            {
                sb.AppendLine(guid);        
            }

            string fileName = fileLocation + "\\generatedGuids-" + DateTime.Now.ToString().Replace("/","-").Replace(" ","-").Replace(":","_") + ".txt";

            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine(sb.ToString());

            file.Close();
        }
    
    }
}