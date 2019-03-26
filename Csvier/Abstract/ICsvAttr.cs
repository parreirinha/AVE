using System;
using System.Collections.Generic;
using System.Linq;
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
        void InvokeMethodForCorrespondence(CsvParser parser);
    }
}
