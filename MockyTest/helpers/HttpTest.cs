using Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocky.Test.helpers
{
    class HttpTest : IHttpRequest
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string GetBody(string url)
        {
            throw new NotImplementedException();
        }
    }
}
