using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Cache
{
    public class ReflectorsCache
    {
        public static Dictionary<Type, IReflection> cache = new Dictionary<Type, IReflection>();

        /*
        
        public IReflection TryGet(Type t)
        {
            IReflection res;
            reflectorsCache.TryGetValue(t, out res);
            return res;
        }

        public bool ContainsKey(Type t)
        {
            return reflectorsCache.ContainsKey(t);
        }

        public void Add(Type t, IReflection reflect)
        {
            reflectorsCache.Add(t, reflect);
        }

        */
    }
}
