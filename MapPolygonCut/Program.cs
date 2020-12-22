using MapPolygonCut.WorkFlow;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace MapPolygonCut
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите запрос");
                string response = Console.ReadLine();

                Console.WriteLine("Введите степень сжатия");
                string strReduction = Console.ReadLine();
                bool isNumeric = int.TryParse(strReduction, out _);
                if (isNumeric == false)
                {
                    throw new Exception("Введено не число");
                }
                int numberReductions = Convert.ToInt32(strReduction);

                Console.WriteLine("Введите имя файла без расширения");
                string fileName = Console.ReadLine();

                JObject rootJson = OSMWorkflow.WorkFlowStart(response, numberReductions, fileName);
                File.WriteAllText(fileName + ".json", rootJson.ToString());
                Console.WriteLine($"Файл находится в папке {Path.GetFullPath(fileName)}.json");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
