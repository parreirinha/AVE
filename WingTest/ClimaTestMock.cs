using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Clima;
using System.Collections.Generic;
using Mocky;

namespace Csvier.Test
{
    [TestClass]
    public class ClimaTestMock
    {

        [TestMethod]
        public void TestLoadSearchOportoMock()
        {
            Mocker mocker = new Mocker(typeof(IWeatherWebApi));
            mocker
                  .When("Search")
                  .With("oporto")
                  .Return(new LocationInfo[] {
                        null,
                        null,
                        null,
                        null,
                        null,
                        new LocationInfo("Cuba", "", 0, 0)});

            IWeatherWebApi api = (IWeatherWebApi)mocker.Create();
            
            LocationInfo[] locals = api.Search("oporto");
            Assert.AreEqual("Cuba", locals[5].Country);
            Assert.AreEqual("", locals[5].Region);
            Assert.AreEqual(0, locals[5].Latitude);
            Assert.AreEqual(0, locals[5].Longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestIWeatherWebApiDisposeNotImplemented()
        {
            Mocker mocker = new Mocker(typeof(IWeatherWebApi));
            mocker
                  .When("Search")
                  .With("oporto")
                  .Return(new LocationInfo[] {null, null, null, null, null, new LocationInfo("Cuba", "", 0, 0)});

            IWeatherWebApi api = (IWeatherWebApi)mocker.Create();
            api.Dispose(); // NotImplementedException
        }



        [TestMethod]
        public void TestLoadPastWeatherOnJanuaryAndCheckMaximumTempCMock()
        {
            //using (WeatherWebApi api = new WeatherWebApi())
            //{
            //    IEnumerable<WeatherInfo> infos = api.PastWeather(37.017, -7.933, DateTime.Parse("2019-01-01"), DateTime.Parse("2019-01-30"));
            //    int max = int.MinValue;
            //    foreach (WeatherInfo wi in infos)
            //    {
            //        if (wi.TempC > max) max = wi.TempC;
            //    }
            //    Assert.AreEqual(19, max);
            //    // Console.WriteLine(String.Join("\n", infos));


            Mocker mocker = new Mocker(typeof(IWeatherWebApi));
            mocker
                    .When("PastWeather")
                    .With(37.017, -7.933, DateTime.Parse("2019-01-01"), DateTime.Parse("2019-01-30"))
                    .Return(
                    new WeatherInfo[] {
                        new WeatherInfo(new DateTime(2019, 01, 1), 17),
                        new WeatherInfo(new DateTime(2019, 01, 2), 15),
                        new WeatherInfo(new DateTime(2019, 01, 3), 12),
                        new WeatherInfo(new DateTime(2019, 01, 4), 19),
                        new WeatherInfo(new DateTime(2019, 01, 5), 21),
                        new WeatherInfo(new DateTime(2019, 01, 6), 1),
                        new WeatherInfo(new DateTime(2019, 01, 7), 19),
                        new WeatherInfo(new DateTime(2019, 01, 8), 17)
                    });

            IWeatherWebApi api = (IWeatherWebApi)mocker.Create();

            WeatherInfo[] res = api.PastWeather(37.017, -7.933, DateTime.Parse("2019-01-01"), DateTime.Parse("2019-01-30"));

            int max = int.MinValue;
            foreach (WeatherInfo wi in res)
            {
                if (wi.TempC > max) max = wi.TempC;
            }

            Assert.AreEqual(21, max);
            Assert.AreEqual(12, res[2].TempC);
            Assert.AreEqual(new DateTime(2019, 01, 1), res[0].Date);
        }


    }
}
