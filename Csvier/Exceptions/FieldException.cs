﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Exceptions
{
    public class FieldException : Exception
    {
        public FieldException(string message) : base(message)
        {
        }

    }
}
