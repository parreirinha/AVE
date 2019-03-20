using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Csvier.Abstract

namespace Csvier.Abstract
{
    interface IReflection
    {
        void SetValue(string name, string data, object target);

        //void Add(string paramName, int col);
    }
}
