using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Parsers
{
    class FieldParser : IParser
    {
        private Type type;
        private Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public FieldParser(Type type)
        {
            this.type = type;
        }

        public void SetValue(string[] lineData, int col, object target)
        {
            throw new NotImplementedException();
        }

        public void Add(string paramName, int col)
        {
            dictionary.Add(paramName, col);
        }
    }
}
