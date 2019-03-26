using Csvier.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Abstract
{
    /**
     * Objects that implements this interface have the hability to call void SetValue(string[] lineData, object target);
     * withch will call the SetValue of reflection API
     * 
     * this wrapper objects contains one object of type IReflection
     * 
     * */
    interface IParserWrapper
    {
        void SetValue(string[] lineData, object target);

        CsvBasicInfo GetBasicInfo();
    }
}
