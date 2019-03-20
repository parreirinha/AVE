using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Exceptions
{
    public class PropertyException : Exception
    {
        public PropertyException(string message) : base(message)
        {
        }
    }
}
