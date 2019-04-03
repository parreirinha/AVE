using Mocky.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmitTest
{
    class Program
    {
        static void Main(string[] args)
        {

 
        }
    }


    class MyClass : ICalculator
    {
        public int Add(int a, int b)
        {
            throw new NotImplementedException();
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
