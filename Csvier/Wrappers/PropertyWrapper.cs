using Csvier.Abstract;
using Csvier.Cache;
using Csvier.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Wrappers
{
    class PropertyWrapper : IWrapper
    {
        PropertyParser pp;
        ParserBasicInfo pbi;


        public PropertyWrapper(PropertyParser pp, ParserBasicInfo pbi)
        {
            this.pp = pp;
            this.pbi = pbi;
        }
    }
}
