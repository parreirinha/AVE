using System;
using Clima;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Csvier.Test
{
    [TestClass]
    public class Part2_FieldsAndProperties
    {

        string sample1;

        [TestInitialize]
        public void TestInitialize()
        {
            sample1 =
                "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

        }
        [TestMethod]
        public void ParseWeatherInfo()
        {

            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo))
                            .CtorArg("date", 0)
                            .CtorArg("tempC", 2)
                            .PropArg("PrecipMM", 11)
                            .PropArg("Desc", 10);

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
    }
}
