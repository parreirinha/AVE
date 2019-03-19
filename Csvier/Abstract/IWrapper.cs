using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Abstract
{
    interface IWrapper
    {
        void AddParser();

        void SetValue(string name, string[] lineData, int col, object target);
    }
}
