using System;
using Clima;
using Csvier.CsvAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Request;

namespace Csvier.Test
{
    [TestClass]
    public class Part4_AttributeTest
    {

        string sample1, sample2;
        IHttpRequest req;

        [TestInitialize]
        public void TestInitialize()
        {
            sample1 =
                "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

            sample2 =
            "# The Search API\n" +
            "# Data returned is laid out in the following order:-\n" +
            "# AreaName    Country     Region(If available)    Latitude    Longitude   Population(if available)    Weather Forecast URL\n" +
            "#\n" +
            "Oporto  Spain Galicia 42.383 - 7.100  0   http://api-cdn.worldweatheronline.com/v2/weather.aspx?q=42.3833,-7.1" + "\n" +
            "Oporto Portugal    Porto   41.150 - 8.617  0   http://api-cdn.worldweatheronline.com/v2/weather.aspx?q=41.15,-8.6167" + "\n" +
            "Oporto South Africa Limpopo -22.667 29.633  0   http://api-cdn.worldweatheronline.com/v2/weather.aspx?q=-22.6667,29.6333" + "\n" +
            "El Oporto   Mexico Tamaulipas  23.266 - 98.768 0   http://api-cdn.worldweatheronline.com/v2/weather.aspx?q=23.2658,-98.7675" + "\n" +
            "Puerto Oporto   Bolivia Pando   -9.933 - 66.417 0   http://api-cdn.worldweatheronline.com/v2/weather.aspx?q=-9.9333,-66.4167" + "\n" +
            "Oporto Cuba    Santiago de Cuba    20.233 - 76.167 0   http://api-cdn.worldweatheronline.com/v2/weather.aspx?q=20.2333,-76.1667" + "\n";
        }
        [TestMethod]
        public void WeatherInfoCustomAttribute()
        {

            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo));

            CsvCorrespondenceAttr.MakeAttributeCorrespondence(pastWeather, typeof(WeatherInfo)); // this makes the correspondence between ctorArgs, fieldArgs and paramArgs

            object[] items = pastWeather
                                .Load(sample1)
                                .Parse();

            DateTime[] dates = {
                DateTime.Parse("2019-01-01"),
                DateTime.Parse("2019-01-02"),
                DateTime.Parse("2019-01-03"),
                DateTime.Parse("2019-01-04")
            };

            Assert.AreEqual(4, items.Length);

            //date
            Assert.AreEqual(17, ((WeatherInfo)items[0]).TempC);
            Assert.AreEqual(18, ((WeatherInfo)items[1]).TempC);
            Assert.AreEqual(16, ((WeatherInfo)items[2]).TempC);
            Assert.AreEqual(16, ((WeatherInfo)items[3]).TempC);

            //tempC
            Assert.AreEqual(dates[0], ((WeatherInfo)items[0]).Date);
            Assert.AreEqual(dates[1], ((WeatherInfo)items[1]).Date);
            Assert.AreEqual(dates[2], ((WeatherInfo)items[2]).Date);
            Assert.AreEqual(dates[3], ((WeatherInfo)items[3]).Date);

            //PrecipMM
            Assert.AreEqual(0.0, ((WeatherInfo)items[0]).PrecipMM);
            Assert.AreEqual(0.0, ((WeatherInfo)items[1]).PrecipMM);
            Assert.AreEqual(0.0, ((WeatherInfo)items[2]).PrecipMM);
            Assert.AreEqual(0.1, ((WeatherInfo)items[3]).PrecipMM);

            //Desc
            Assert.AreEqual("Partly cloudy", ((WeatherInfo)items[0]).Desc);
            Assert.AreEqual("Partly cloudy", ((WeatherInfo)items[1]).Desc);
            Assert.AreEqual("Sunny", ((WeatherInfo)items[2]).Desc);
            Assert.AreEqual("Partly cloudy", ((WeatherInfo)items[3]).Desc);

        }


        [TestMethod]
        public void LocationInfoCustomAttribute()
        {
            const string WEATHER_KEY = "72dbd0b57ebb460c861120116191303";
            const string WEATHER_HOST = "http://api.worldweatheronline.com/premium/v1/";
            const string SEARCH = WEATHER_HOST + "search.ashx?query={0}&format=tab&key=" + WEATHER_KEY;
            req = new HttpRequest();

            string path = SEARCH.Replace("{0}", "oporto");

            string body = req.GetBody(path);

            CsvParser location = new CsvParser(typeof(LocationInfo), '\t');

            CsvCorrespondenceAttr.MakeAttributeCorrespondence(location, typeof(LocationInfo)); // this makes the correspondence between ctorArgs, fieldArgs and paramArgs

            object[] locationInfo = location
                    .Load(body)
                    .Remove(4)
                    .RemoveEmpties()
                    .Parse();

            Assert.AreEqual(6, locationInfo.Length);
        }
    }
}
