using Clima;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Test
{
    public interface IWeatherWebApi : IDisposable
    {
        WeatherInfo[] PastWeather(double lat, double log, DateTime from, DateTime to);
        LocationInfo[] Search(string query);
    }
}
