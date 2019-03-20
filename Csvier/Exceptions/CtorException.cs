using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Exceptions
{
    public class CtorException : Exception
    {
        public CtorException(string message) : base(message)
        {
        }

    }
}
