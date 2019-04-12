using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mocky.helpers
{
    public class MockCalculator : MocksBase, ICalculator
    {
        

        public MockCalculator(MockMethod[] ms) : base(ms)
        {
            
        }

        public int Add(int a, int b)
        {
            object[] mockMethodKey = new object[] { a, b };
            MethodBase currentMethodInfo = MethodBase.GetCurrentMethod();
            MockMethod mockMethod = Contains(currentMethodInfo.Name, mockMethodKey);

            if(mockMethod == null)
                throw new NotImplementedException();

            return (int) mockMethod.Call(a, b);

        }

        public int Div(int a, int b)
        {
            throw new NotImplementedException();
        }

        public int Mul(int a, int b)
        {
            throw new NotImplementedException();
        }

        public int Sub(int a, int b)
        {
            throw new NotImplementedException();
        }


    }
}
