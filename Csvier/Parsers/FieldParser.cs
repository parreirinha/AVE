using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Parsers
{
    class FieldParser : IParser
    {
        private Type type;
        private string name;
        private int col;
        private char separator;
        //private Dictionary<string, int> dictionary = new Dictionary<string, int>();

        public FieldParser(Type type, string name, int col, char sep)
        {
            this.type = type;
            this.name = name;
            this.col = col;
            separator = sep;
        }

        public void SetValue(string[] lineData, int col, object target)
        {
            FieldInfo fieldInfo = type.GetField(name);
            fieldInfo.SetValue(target, GetValue(fieldInfo, lineData[col]));
        }

        //returns the value of the field using parse
        private object GetValue(FieldInfo fieldInfo, string data)
        {
            throw new NotImplementedException();
        }

        /*public void Add(string paramName, int col)
        {
            dictionary.Add(paramName, col);
        }*/
    }
}
