using Csvier.Abstract;
using System;
using System.Reflection;

namespace Csvier.Reflect
{
    class FieldReflect : IReflection
    {
        private Type type;
        private MethodInfo mi;

        public FieldReflect(Type type)
        {
            this.type = type;
            mi = type.GetMethod("Parse", new Type[] { typeof(string) });
        }

        public void SetValue(Type fType, string name, string data, object target)
        {
            FieldInfo fieldInfo = fType.GetField(name);
            fieldInfo.SetValue(target, GetValue(data));
        }

        //returns the value of the field using parse
        private object GetValue(string data)
        {
            object val = mi.Invoke(type, new object[] { data });

            return val;
        }

    }
}
