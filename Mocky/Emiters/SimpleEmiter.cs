﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mocky.Emiters
{


    public class SimpleEmiter
    {
        private AssemblyName aName;
        private AssemblyBuilder ab;
        private ModuleBuilder mb;
        private TypeBuilder tb;
        private ConstructorBuilder cb;
        Type type;

        public SimpleEmiter(Type type, string assemblyName, string typeName) {

            aName = new AssemblyName(assemblyName);
            ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            tb = mb.DefineType(typeName, TypeAttributes.Public);
            tb.AddInterfaceImplementation(type);
            this.type = type;
        }

        public void BuildConstructorWithoutParameters()
        {
            Type[] parameterTypes = new Type[] { typeof(MockMethod[]) };//Type.EmptyTypes;

            cb = tb.DefineConstructor(
                MethodAttributes.Public, 
                CallingConventions.Standard, 
                parameterTypes);

            ILGenerator il = cb.GetILGenerator();
            ConstructorInfo ci = typeof(object).GetConstructor(Type.EmptyTypes);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, ci);
            il.Emit(OpCodes.Ret);
        }

        public void BuildMethods()
        {
            MethodInfo[] mi = type.GetMethods();

            foreach(MethodInfo mInfo in mi)
            {
                CreateMethod(mInfo);
            }

        }

        private void CreateMethod(MethodInfo mi)
        {
            MethodAttributes mAttributes =
                    MethodAttributes.Public |
                    MethodAttributes.ReuseSlot |
                    MethodAttributes.HideBySig |
                    MethodAttributes.Virtual;

            Type retType = mi.ReturnType;
            Type[] parameterType = GetTypeArrayOfMethodParameter(mi);//new Type[]{ typeof(int), typeof(int) };

            MethodBuilder mb = tb.DefineMethod(
                    mi.Name,
                    mAttributes,
                    retType,
                    parameterType);

            ILGenerator il = mb.GetILGenerator();
            ConstructorInfo ci = typeof(NotImplementedException).GetConstructor(Type.EmptyTypes);

            il.Emit(OpCodes.Newobj, ci);
            il.Emit(OpCodes.Ret);
        }

        private Type[] GetTypeArrayOfMethodParameter(MethodInfo mi)
        {
            ParameterInfo[] pi = mi.GetParameters();
            Type[] res = new Type[pi.Length];

            for(int i = 0; i<pi.Length; i++)
            {
                res[i] = pi[i].ParameterType;
            }
            return res;
        }

        public Type CreateType()
        {
            Type type = tb.CreateType();
            ab.Save(aName.Name + ".dll");
            return type;
        }
    }
}
