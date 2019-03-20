using Csvier.Abstract;
using System;
using System.Reflection;

namespace Csvier.Reflect
{
    class FieldReflect : IReflection
    {
        private Type type;

        public FieldReflect(Type type)
        {
            this.type = type;
        }

        public void SetValue(string name, string data, object target)
        {
            FieldInfo fieldInfo = type.GetField(name);
            fieldInfo.SetValue(target, GetValue(fieldInfo, data));
        }

        //returns the value of the field using parse
        private object GetValue(FieldInfo fieldInfo, string data)
        {
            // do parse here like in CtorManager
            throw new NotImplementedException();
        }

    }
}
