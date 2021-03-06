﻿using Csvier.Abstract;
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
        MethodInfo mi;

        public PropertyReflect(Type type)
        {
            this.type = type;
            mi = type.GetMethod("Parse", new Type[] { typeof(string) });
        }

        public void SetValue(Type pType, string name, string data, object target)
        {
            PropertyInfo propertyInfo = pType.GetProperty(name);
            propertyInfo.SetValue(target, GetValue(data));
        }

        //returns the value of the field using parse
        private object GetValue(string data)
        {
            if (type == typeof(string))
                return data;
            
            object val = mi.Invoke(type, new string[] { data });

            return val;
        }

    }
}


