using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocky.Emiters
{
    public class MethodBuilderTest
    {

        private Dictionary<object[], object> results;

        public MethodBuilderTest(Dictionary<object[], object> results)
        {
            this.results = results;
        }

        public object Call(params object[] args)
        {
            foreach (KeyValuePair<object[], object> pair in results)
            {
                Object[] arg = pair.Key;
                Object val = pair.Value;
                bool find = true;

                for (int i = 0; i < arg.Length; i++)
                {
                    Type t = (args[i]).GetType();

                    var a = Convert.ChangeType(arg[i], t);
                    var b = Convert.ChangeType(args[i], t);

                    if (!a.Equals(b))
                    {
                        find = false;
                        break;
                    }
                }

                if (find)
                    return val;
            }
            return 0;
        }
    }
}
