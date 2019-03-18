using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Abstract
{
    interface IParser
    {
        void SetValue(string[] lineData, int col, object target);

        void Add(string paramName, int col);
    }
}
