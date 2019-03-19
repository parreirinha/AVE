using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Cache
{
    public static class ParserCache
    {
        public static Dictionary<Type, IParser> parser = new Dictionary<Type, IParser>();
    }
}
