using Csvier.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier.Reflect
{
    class PropertyReflect : IReflection
    {
        Type type;

        public PropertyReflect(Type type)
        {
            this.type = type;
        }

        public void SetValue(Type pType, string name, string data, object target)
        {
            PropertyInfo propertyInfo = pType.GetProperty(name);
            propertyInfo.SetValue(target, GetValue(data));
        }

        private object GetValue(string data)
        {
            if (type == typeof(string))
                return data;

            MethodInfo mi = type.GetMethod("Parse", new Type[] { typeof(string) });
            object val = mi.Invoke(type, new string[] { data });

            return val;

        }

    }
}


