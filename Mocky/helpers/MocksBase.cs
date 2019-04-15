using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mocky.helpers
{
    public class MocksBase
    {
        public MockMethod[] ms;

        public MocksBase(MockMethod[] ms)
        {
            this.ms = ms;
        }

        protected MockMethod Contains(string methodName, params object[] args)
        {
            return null;
        }

        protected object[] BuildParametersObjectArray(params object[] param)
        {

            object[] res = new object[param.Length];
            int idx = 0;

            foreach(object p in param)
            {
                res[idx++] = p;
            }

            return res;
        }
    }
}
