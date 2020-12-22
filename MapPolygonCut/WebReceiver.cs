using System.IO;
using System.Net;

namespace MapPolygonCut
{
    class WebReceiver
    {
        /// <summary>
        /// Получение JSON объекта с сайта
        /// </summary>
        /// <param name="url"></param>
        public WebReceiver(string url)
        {
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            } 

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";
            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    JsonString = sr.ReadToEnd();
                }
            }
        }

        public string JsonString { get; }
    }
}
