﻿using System;
using Clima;
using Csvier.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocky;
using System.Collections.Generic;
using System.Linq;

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

            Mocker mocker = new Mocker(typeof(CsvParser<WeatherInfo>));

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

            CsvParser<WeatherInfo> api = (CsvParser< WeatherInfo>)mocker.Create();

            IEnumerable<WeatherInfo> items = api.Parse();

            DateTime[] dates = {
                DateTime.Parse("2019-01-01"),
                DateTime.Parse("2019-01-02"),
                DateTime.Parse("2019-01-03"),
                DateTime.Parse("2019-01-04")
            };

            int[] temps = new int[] { 17, 18, 16, 16 };
            int idx = 0;

            foreach(WeatherInfo wi in items)
            {
                Assert.AreEqual(temps[idx], wi.TempC);
                Assert.AreEqual(dates[idx++], wi.Date);
            }

            Assert.AreEqual(4, items.Count());
        }
    }
}
