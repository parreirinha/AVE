using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Csvier.Abstract

namespace Csvier.Abstract
{
    /**
     * Calls SetValue of corresponding objects that implements this interface.
     * There are two types of objects that implements IReflect:
     *      #FieldReflect
     *      #PropReflect
     * 
     * */
    public interface IReflection
    {
        void SetValue(Type type, string name, string data, object target);


    }
}
