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

        public FieldParser(Type type)
        {
            this.type = type;
        }

        public void SetValue(string name, string[] lineData, int col, object target)
        {
            FieldInfo fieldInfo = type.GetField(name);
            fieldInfo.SetValue(target, GetValue(fieldInfo, lineData[col]));
        }

        //returns the value of the field using parse
        private object GetValue(FieldInfo fieldInfo, string data)
        {
            throw new NotImplementedException();
        }

    }
}
