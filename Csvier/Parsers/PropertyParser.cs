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

        public PropertyParser(Type type)
        {
            this.type = type;
        }

        public void SetValue(string name, string[] lineData, int col, object target)
        {
            PropertyInfo propertyInfo = type.GetProperty(name);
            propertyInfo.SetValue(target, GetValue(propertyInfo, lineData[col]));
        }

        private object GetValue(PropertyInfo propertyInfo, string data)
        {
            throw new NotImplementedException();
        }

    }
}
