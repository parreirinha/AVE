using Csvier.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Abstract
{
    interface IParserWrapper
    {
        void SetValue(string[] lineData, object target);

        CsvBasicInfo GetBasicInfo();
    }
}
