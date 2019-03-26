using Csvier;
using Request;
using System;
using System.Linq;

namespace ClimateDataAPI
{
    public class ClimateDataApi : IDisposable
    {
        
        const string HOST = "http://climatedataapi.worldbank.org/climateweb/rest/v1/";
        const string PATH = HOST + "country/mavg/{0}/{1}/{2}/PRT.csv";
        readonly IHttpRequest req;
        public enum VALUES  {PR = 1, TAS = 2}

        public ClimateDataApi() : this(new HttpRequest())
        {
        }

        public ClimateDataApi(IHttpRequest req)
        {
            this.req = req;
        }

        public void Dispose()
        {
            req.Dispose();
        }

        public ClimateValues[] GetClimateValues(VALUES options, int dateFrom, int dateTo )
        {
            string dataType = (options == VALUES.PR ? "pr" : (options == VALUES.TAS ? "tas" : null));
            if (dataType == null)
                throw new Exception("illegal argument on GetClimateValues, choose between VALUES.PT for precepitation and VALUES.TAS for temperature");

            string request = PATH
                .Replace("{0}", dataType)
                .Replace("{1}", dateFrom.ToString())
                .Replace("{2}", dateTo.ToString());

            string body = req.GetBody(request);


            CsvParser values =
                new CsvParser(typeof(ClimateValues))
                    .CtorArg("gCM", 0)
                    .CtorArg("variableType", 1)
                    .CtorArg("jan", 4)
                    .CtorArg("feb", 5)
                    .CtorArg("mar", 6);

            Object[] items = values
                    .Load(body)
                    .Remove(1)
                    .RemoveEmpties()
                    .Parse();

            return items.Select(x => (ClimateValues)x).ToArray();

        }
    }
}
