using MapPolygonCut.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MapPolygonCut.WorkFlow
{
    public class OSMWorkflow
    {
        public JObject objectJson;

        public OSMWorkflow(string response, int numberReductions, string fileName)
        {
            objectJson = WorkFlowStart(response, numberReductions,fileName);
        }


        /// <summary>
        /// Парсинг полученного JSON файла
        /// </summary>
        /// <param name="response"></param>
        /// <param name="numberReductions"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static JObject WorkFlowStart(string response, int numberReductions, string fileName)
        {
            string url = $"https://nominatim.openstreetmap.org/search.php?q={response}&polygon_geojson=1&format=jsonv2";
            WebReceiver webReceiver = new WebReceiver(url);
            JArray array = JsonConvert.DeserializeObject<JArray>(webReceiver.JsonString);
         
            
            JObject rootJson = new JObject();
            JArray resultsArray = new JArray();
            

            foreach (var item in array)
            {
                string displayName = item[OSM.display_name].ToString();
                JObject itemJObject = new JObject();
                itemJObject[OSM.display_name] = displayName;
                JArray typeJArray = new JArray();
                JArray coordinatesJArray = new JArray();

                string resultType = item[OSM.geojson][OSM.type].ToString();

                JToken points = item[OSM.geojson][OSM.coordinates];

                // Добавление в массив типа Points
                if (resultType == OSM.pointConst)
                {
                    typeJArray.Add(resultType);
                    coordinatesJArray.Add(points);
                    
                }
                /// Добавление в массив типа LineString
                else if (resultType == OSM.lineStringConst)
                {
                    typeJArray.Add(resultType);
                    var listCollectionOutput = ReducerList.Reduce(numberReductions, points);
                    coordinatesJArray.Add(listCollectionOutput);
                }
                //Добавление в массив типа Polygon или MultiLineString
                else if (resultType == OSM.polygonConst || resultType == OSM.multiLineStringConst)
                {
                    typeJArray.Add(resultType);
                    foreach (var listCollectionCoordinates in points)
                    {
                        var listCollectionOutput = ReducerList.Reduce(numberReductions, listCollectionCoordinates);
                        coordinatesJArray.Add(new JArray(listCollectionOutput));
                    }
                }
                // Добавление в массив типа MultiPolygon
                else if (resultType == OSM.multiPolygonConst)
                {
                    typeJArray.Add(resultType);
                    foreach (var listCollectionCoordinates in points)
                    {
                        JArray polyCoordinatesArray = new JArray();
                        foreach (var itemColl in listCollectionCoordinates)
                        {
                            dynamic listCollectionOutput = ReducerList.Reduce(numberReductions, itemColl);
                            polyCoordinatesArray.Add(listCollectionOutput);
                        }
                        coordinatesJArray.Add(polyCoordinatesArray);
                    }
                }
                // Добавление в JSON типа
                itemJObject[OSM.type] = typeJArray;
                // Добавление в JSON массива с координатами
                itemJObject[OSM.coordinates] = coordinatesJArray;

                resultsArray.Add(itemJObject);

                rootJson[OSM.results] = resultsArray;
            }

            
            return rootJson;
        }
    }
}
