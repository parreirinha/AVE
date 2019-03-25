using System;
using System.Collections.Generic;
using System.Text;

namespace ClimateDataAPI
{
    class ClimateValues
    {
        string GCM;
        string variableType;
        DateTime from_year;
        DateTime to_year;
        int Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec;

        public ClimateValues(string gCM, string variableType, int jan, int feb, int mar)
        {
            GCM = gCM;
            this.variableType = variableType;
            Jan = jan;
            Feb = feb;
            Mar = mar;
        }

        public ClimateValues(string gCM, DateTime from_year, DateTime to_year, int jul, int aug)
        {
            GCM = gCM;
            this.from_year = from_year;
            this.to_year = to_year;
            Jul = jul;
            Aug = aug;
        }

        public ClimateValues(string gCM, string variableType, DateTime from_year, DateTime to_year, int jan, int jun)
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
