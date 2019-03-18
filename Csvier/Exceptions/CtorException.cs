using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Exceptions
{
    class CtorException : Exception
    {
        public CtorException(string message) : base(message)
        {
        }

    }
}
