using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Parsers
{
    class PropertyParser : IParser
    {
        Type type;
        private Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public PropertyParser(Type type)
        {
            this.type = type;
        }

        public void SetValue(string[] lineData, int col, object target)
        {
            throw new NotImplementedException();
        }

        public void Add(string paramName, int col)
        {
            // adicionar uma validação => ver se existe a propriedade em type
            dictionary.Add(paramName, col);
        }
    }
}
