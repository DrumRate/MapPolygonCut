using System;
using System.Collections.Generic;
using System.Text;

namespace MapPolygonCut.Models
{
    public class OSM
    {
        public const string display_name = "display_name";
        public const string geojson = "geojson";
        public const string type = "type";
        public const string coordinates = "coordinates";
        public const string results = "results";

        public const string pointConst = "Point";
        public const string lineStringConst = "LineString";
        public const string polygonConst = "Polygon";
        public const string multiPolygonConst = "MultiPolygon";
        public const string multiLineStringConst = "MultiLineString";
    }
}
