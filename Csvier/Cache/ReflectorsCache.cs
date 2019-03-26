using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Cache
{
    /**
     * Cache for object of type IReflection, in order to prevent duplication of reflection in objects of same type
     * 
     * */
    public class ReflectorsCache
    {
        public static Dictionary<Type, IReflection> cache = new Dictionary<Type, IReflection>();
    }
}
