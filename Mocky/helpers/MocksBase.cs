using System;
using System.Collections.Generic;
using System.Linq;
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

        public MockMethod Contains(string methodName, params object[] args)
        {
            return null;
        }
    }
}
