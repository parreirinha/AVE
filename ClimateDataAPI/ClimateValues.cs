using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateDataAPI
{
    public class ClimateValues
    {
        public string GCM;
        public string variableType;
        public DateTime from_year;
        public DateTime to_year;
        public double Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec;

        public ClimateValues(string gCM, string variableType, double jan, double feb, double mar)
        {
            GCM = gCM;
            this.variableType = variableType;
            Jan = jan;
            Feb = feb;
            Mar = mar;
        }

        public ClimateValues(string gCM, DateTime from_year, DateTime to_year, double jul, double aug)
        {
            GCM = gCM;
            this.from_year = from_year;
            this.to_year = to_year;
            Jul = jul;
            Aug = aug;
        }

        public ClimateValues(string gCM, string variableType, DateTime from_year, DateTime to_year, double jan, double jun)
        {
            GCM = gCM;
            this.variableType = variableType;
            this.from_year = from_year;
            this.to_year = to_year;
            Jan = jan;
            Jun = jun;
        }
    }

    
}
