using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Parsers
{
    class PropertyParser : IParser
    {
        Type type;
        private string name;
        private int col;
        //private Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public PropertyParser(Type type, string name, int col)
        {
            this.type = type;
            this.name = name;
            this.col = col;
        }

        public void SetValue(string[] lineData, int col, object target)
        {
            PropertyInfo propertyInfo = type.GetProperty(name);
            propertyInfo.SetValue(target, GetValue(propertyInfo, lineData[col]));
        }

        private object GetValue(PropertyInfo propertyInfo, string data)
        {
            throw new NotImplementedException();
        }

        /*public void Add(string paramName, int col)
        {
            // adicionar uma validação => ver se existe a propriedade em type
            dictionary.Add(paramName, col);
        }*/
    }
}
