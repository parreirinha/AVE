using Csvier;
using Csvier.CsvAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima
{

    [CtorArg(typeof(CsvParser<LocationInfo>), "country", 0)]
    [CtorArg(typeof(CsvParser<LocationInfo>), "region", 2)]
    [CtorArg(typeof(CsvParser<LocationInfo>), "latitude", 3)]
    [CtorArg(typeof(CsvParser<LocationInfo>), "longitude", 4)]
    public class LocationInfo
    {
        public String Country { get; set; }
        public String Region { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationInfo()
        {
        }

        public LocationInfo(string country, string region, double latitude, double longitude)
        {
            Country = country;
            Region = region;
            Latitude = latitude;
            Longitude = longitude;
        }

    }
}
