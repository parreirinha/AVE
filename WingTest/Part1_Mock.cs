﻿using System;
using Clima;
using Csvier.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocky;

namespace Csvier.Test
{
    [TestClass]
    public class Part1_Mock
    {


        [TestMethod]
        public void TestWeatherInfoWorkExample()
        {
            string sampleWeatherInLisbonFiltered =
                @"2019-01-01,24,17,63,6,10,74,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,59,10,1031,43,14,57,6,43,13,56,11,17,13,56\n" +
                "2019-01-02,24,18,64,6,9,179,S,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.0,57,10,1030,15,14,57,6,42,13,56,11,17,13,56\n" +
                "2019-01-03,24,16,60,7,11,89,E,113,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0001_sunny.png," + "Sunny,0.0,67,10,1026,3,13,55,7,45,12,54,11,18,12,54\n" +
                "2019-01-04,24,16,60,9,15,78,ENE,116,http://cdn.worldweatheronline.net/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png," + "Partly cloudy,0.1,73,10,1028,27,14,57,9,48,13,55,14,23,13,55";

            Mocker mocker = new Mocker(typeof(ICsvParser));

            mocker
                .When("Parse")
                .With()
                .Return(new object[] {
                    new WeatherInfo(new DateTime(2019, 1, 1), 17),
                    new WeatherInfo(new DateTime(2019, 1, 2), 18),
                    new WeatherInfo(new DateTime(2019, 1, 3), 16),
                    new WeatherInfo(new DateTime(2019, 1, 4), 16)
                });

            mocker
                .When("Load")
                .With(sampleWeatherInLisbonFiltered)
                .Return(null);

            ICsvParser api = (ICsvParser)mocker.Create();

            object[] items = api.Parse();

            DateTime[] dates = {
                DateTime.Parse("2019-01-01"),
                DateTime.Parse("2019-01-02"),
                DateTime.Parse("2019-01-03"),
                DateTime.Parse("2019-01-04")
            };

            Assert.AreEqual(4, items.Length);

            Assert.AreEqual(17, ((WeatherInfo)items[0]).TempC);
            Assert.AreEqual(18, ((WeatherInfo)items[1]).TempC);
            Assert.AreEqual(16, ((WeatherInfo)items[2]).TempC);
            Assert.AreEqual(16, ((WeatherInfo)items[3]).TempC);

            Assert.AreEqual(dates[0], ((WeatherInfo)items[0]).Date);
            Assert.AreEqual(dates[1], ((WeatherInfo)items[1]).Date);
            Assert.AreEqual(dates[2], ((WeatherInfo)items[2]).Date);
            Assert.AreEqual(dates[3], ((WeatherInfo)items[3]).Date);
        }

    }
}
