using Csvier;
using Csvier.CsvAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima
{
    [CtorArg(typeof(CsvParser), "date", 0)]
    [CtorArg(typeof(CsvParser), "tempC", 2)]
    [PropertyArg(typeof(CsvParser), "PrecipMM", 11)]
    [PropertyArg(typeof(CsvParser), "Desc", 10)]
    public class WeatherInfo
    {
        public DateTime Date { get;  }
        public int TempC { get; }
        public double PrecipMM { get; set; }
        public String Desc { get; set; }

        public WeatherInfo()
        {
        }

        public WeatherInfo(DateTime date)
        {
            this.Date = date;
        }

        public WeatherInfo(DateTime date, int tempC)
        {
            this.Date = date;
            this.TempC = tempC;
        }

        public override String ToString()
        {
            return "WeatherInfo{" +
                "date=" + Date +
                ", tempC=" + TempC +
                ", precipMM=" + PrecipMM +
                ", desc='" + Desc + '\'' +
                '}';
        }

    }
}
