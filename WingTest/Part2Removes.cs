using System;
using Clima;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Csvier.Test
{
    [TestClass]
    public class Part2Removes
    {
        string sample1, sample2, sample3, sample4;

        [TestInitialize]
        public void TestInitialize()
        {
            sample1 =
                "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

            sample2 ="# The CSV format is in following way:-\n" +
                "# The day information is available in following format:-\n" +
                "# date,maxtempC,maxtempF,mintempC,mintempF,sunrise,sunset,moonrise,moonset,moon_phase,moon_illumination\n" +
                "#\n" +
                "# Hourly information follows below the day in the following way:-\n" + 
                "# date,time,tempC,tempF,windspeedMiles,windspeedKmph,winddirdegree,winddir16point,weatherCode,weatherIconUrl,weatherDesc,precipMM,humidity,visibilityKm,pressureMB,cloudcover,HeatIndexC,HeatIndexF,DewPointC,DewPointF,WindChillC,WindChillF,WindGustMiles,WindGustKmph,FeelsLikeC,FeelsLikeF\n" +
                "#\n" +
                "Not Available\n" +
                "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

            sample3 = "Not Available\n" +
                "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

            sample4 = 
                "2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "\n"+
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "\n" + 
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

        }

        [TestMethod]
        public void RemoveOddIndexes()
        {
            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo))
                .CtorArg("date", 0)
                .CtorArg("tempC", 2);

            object[] items = pastWeather
                            .Load(sample1)
                            .RemoveOddIndexes()
                            .Parse();

            Assert.AreEqual(18, ((WeatherInfo)items[0]).TempC);
            Assert.AreEqual(16, ((WeatherInfo)items[1]).TempC);

            Assert.AreEqual(2, items.Length);
        }


        [TestMethod]
        public void RemoveEvenIndexes()
        {
            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo))
                .CtorArg("date", 0)
                .CtorArg("tempC", 2);

            object[] items = pastWeather
                            .Load(sample1)
                            .RemoveEvenIndexes()
                            .Parse();

            Assert.AreEqual(17, ((WeatherInfo)items[0]).TempC);
            Assert.AreEqual(16, ((WeatherInfo)items[1]).TempC);

            Assert.AreEqual(2, items.Length);
        }

        [TestMethod]
        public void RemoveWith()
        {
            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo))
                .CtorArg("date", 0)
                .CtorArg("tempC", 2);

            object[] items = pastWeather
                            .Load(sample3)
                            .RemoveWith("Not Available")
                            .Parse();

            Assert.AreEqual(4, items.Length);
        }

        [TestMethod]
        public void Remove()
        {
            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo))
                .CtorArg("date", 0)
                .CtorArg("tempC", 2);

            object[] items = pastWeather
                            .Load(sample2)
                            .Remove(8)
                            .Parse();

            Assert.AreEqual(4, items.Length);
        }

        [TestMethod]
        public void RemoveEmpties()
        {
            CsvParser pastWeather = new CsvParser(typeof(WeatherInfo))
                .CtorArg("date", 0)
                .CtorArg("tempC", 2);

            object[] items = pastWeather
                            .Load(sample4)
                            .RemoveEmpties()
                            .Parse();

            Assert.AreEqual(4, items.Length);
        }



    }
}
