using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Abstract
{
    /**
     * define a method that calls the correspondent method to add ctor, prop or fields
     * EXAMPLE:
     * in CtorArgAttribute calls method CtorArgs of CsvParser
     * in FieldArgAttribute calls method PropArgs of CsvParser
     * in PropArgAttribute calls method PropArgs of CsvParser
     * */
    public interface ICsvAttr
    {
        string Name { get; set; }
        int Column { get; set; }
        MethodInfo mi { get; set; }
        //void InvokeMethodForCorrespondence(CsvParser<T> parser);
    }
}
