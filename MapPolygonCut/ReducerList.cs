using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace MapPolygonCut
{
    class ReducerList
    {
        /// <summary>
        /// Сокращение количества координат
        /// </summary>
        /// <param name="digit">Частота точек</param>
        /// <param name="inputList">Входные значения точек</param>
        /// <returns></returns>
        public static List<JToken> Reduce(int digit, JToken inputList)
        {
            List<JToken> outputList = new List<JToken>();
            for (int i = 0; i < inputList.Count(); i += digit)
            {
                outputList.Add(inputList[i]);
            }
            return outputList;
        }
    }
}
