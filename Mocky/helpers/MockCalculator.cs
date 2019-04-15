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
            return (int) InvokeMethod("Add", a, b);
        }


        public int Div(int a, int b)
        {
            return (int)InvokeMethod("Div", a, b);
        }

        public int Mul(int a, int b)
        {
            return (int)InvokeMethod("Mul", a, b);
        }

        public int Sub(int a, int b)
        {
            return (int)InvokeMethod("Sub", a, b);
        }


    }
}
