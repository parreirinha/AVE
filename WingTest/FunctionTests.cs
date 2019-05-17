using System;
using Clima;
using Csvier.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Csvier.GenericTest
{

    [TestClass]
    public class FunctionTests
    {
        static int counter = 0;
       
        static WeatherInfo EagerFunction(string str)
        {
            counter++;
            return null;
        }

        static WeatherInfo FunctionParser(string str)
        {

            string[] data = str.Split(',');

            DateTime date = Convert.ToDateTime(data[0]);//DateTime.Parse(data[0]);
            int tempc = int.Parse(data[2]);
            double precipMM = double.Parse(data[11]);
            string desc = data[10];

            WeatherInfo retObj = new WeatherInfo(date, tempc);
            retObj.PrecipMM = precipMM;
            retObj.Desc = desc;

            return retObj;
        }

        string sampleWeatherInLisbonFiltered =
            @"2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56
            2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56
            2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png,Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54
            2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png,Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

        double[] prec = new double[] { 0.0, 0.0, 0.0, 0.1 };
        string[] descs = new string[] { "Partly cloudy", "Partly cloudy", "Sunny", "Partly cloudy" };

        int[] temps = new int[] { 17, 18, 16, 16};
        DateTime[] dates = new DateTime[] {
            Convert.ToDateTime("2019-01-01"),
            Convert.ToDateTime("2019-01-02"),
            Convert.ToDateTime("2019-01-03"),
            Convert.ToDateTime("2019-01-04")
        };

        [TestMethod]
        public void TestFunctionWeatherInfo()
        {

            CsvParser<WeatherInfo> pastWeather = new CsvParser<WeatherInfo>()
                            .CtorArg("date", 0)
                            .CtorArg("tempC", 2);

            Func<string, WeatherInfo> wp = FunctionParser;

            WeatherInfo[] items = pastWeather
                            .Load(sampleWeatherInLisbonFiltered)
                            .RemoveEmpties()
                            .Parse(wp);
            int idx = 0;
            foreach(WeatherInfo wi in items)
            {
                Assert.AreEqual(prec[idx], items[idx].PrecipMM);
                Assert.AreEqual(descs[idx], items[idx].Desc);
                Assert.AreEqual(dates[idx], items[idx].Date);
                Assert.AreEqual(temps[idx], items[idx++].TempC);
            }
            
        }



        [TestMethod]
        public void EagerTest()
        {

            CsvParser<WeatherInfo> pastWeather = new CsvParser<WeatherInfo>()
                            .CtorArg("date", 0)
                            .CtorArg("tempC", 2);

            Func<string, WeatherInfo> wp = EagerFunction;

            WeatherInfo[] items = pastWeather
                            .Load(sampleWeatherInLisbonFiltered)
                            .RemoveEmpties()
                            .Parse(wp);

            Assert.AreEqual(4, items.Length);
        }
    }
}
