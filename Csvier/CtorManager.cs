using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier
{
    class CtorManager
    {

        private Type type;
        private Dictionary<String, int> ctorParams = new Dictionary<string, int>();

        public CtorManager(Type type)
        {
            this.type = type;
        }

        public object BuildInstance()
        {
            throw new NotImplementedException();
        }

        public void AddCtorParameter(string paramName, int col)
        {
            ctorParams.Add(paramName, col);
        }


    }
}
