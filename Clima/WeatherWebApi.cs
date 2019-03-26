using Csvier;
using Request;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima
{
    public class WeatherWebApi : IDisposable
    {
        const string WEATHER_KEY = "72dbd0b57ebb460c861120116191303";
        const string WEATHER_HOST = "http://api.worldweatheronline.com/premium/v1/";
        const string PATH_WEATHER = WEATHER_HOST + "past-weather.ashx?q={0},{1}&date={2}&enddate={3}&tp=24&format=csv&key=" + WEATHER_KEY;
        const string SEARCH = WEATHER_HOST + "search.ashx?query={0}&format=tab&key=" + WEATHER_KEY;

        readonly CsvParser pastWeather;
        readonly CsvParser locations;
        readonly IHttpRequest req;

        public WeatherWebApi() : this(new HttpRequest())
        {
        }

        public WeatherWebApi(IHttpRequest req)
        {
            this.req = req;
        }

        public void Dispose()
        {
            req.Dispose();
        }

        public WeatherInfo[] PastWeather(double lat, double log, DateTime from, DateTime to)
        {
            string latStr = lat.ToString("0.000", CultureInfo.InvariantCulture);
            string logStr = log.ToString("0.000", CultureInfo.InvariantCulture);

            string dateFrom= FormatDates(from);
            string dateTo = FormatDates(to);

            string path = PATH_WEATHER
                            .Replace("{0}", latStr)
                            .Replace("{1}", logStr)
                            .Replace("{2}", dateFrom)
                            .Replace("{3}", dateTo);

            string body = req.GetBody(path);

            CsvParser weather = 
                new CsvParser(typeof(WeatherInfo))
                    .CtorArg("date", 0)
                    .CtorArg("tempC", 2)
                    .PropArg("PrecipMM", 11)
                    .PropArg("Desc", 10);

            Object[] items = weather
                    .Load(body)
                    .RemoveWith("#")
                    .Remove(1)
                    .RemoveEmpties()
                    .RemoveOddIndexes()
                    .Parse();

            return items.Select(x => (WeatherInfo)x).ToArray();
        }

        private string FormatDates(DateTime date)
        {
            return date.Day + "-" + date.Month + "-" + date.Year;
        }

        public LocationInfo[] Search(string query) {

            string path = SEARCH.Replace("{0}", query);

            string body = req.GetBody(path);

            CsvParser location = new CsvParser(typeof(LocationInfo), '\t')
                    .CtorArg("country", 1)
                    .CtorArg("region", 2)
                    .CtorArg("latitude", 3)
                    .CtorArg("longitude", 4);

            object[] locationInfo = location
                    .Load(body)
                    .Remove(4)
                    .RemoveEmpties()
                    .Parse();

            return locationInfo.Select(x => (LocationInfo)x).ToArray();
        }   
    }
}
