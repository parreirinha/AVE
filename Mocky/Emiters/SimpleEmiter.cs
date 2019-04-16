using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mocky.Emiters
{

    public class SimpleEmiter
    {
        private AssemblyName aName;
        private AssemblyBuilder ab;
        private ModuleBuilder mb;
        private TypeBuilder tb;
        private ConstructorBuilder cb;
        private FieldBuilder callReturnValue;
        Type type;

        public SimpleEmiter(Type type, string assemblyName) {

            aName = new AssemblyName(assemblyName);
            ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            tb = mb.DefineType("Mock" + type.Name, TypeAttributes.Public | TypeAttributes.Class, typeof(helpers.MocksBase));
            tb.AddInterfaceImplementation(type);

            Type[] interfaces = type.GetInterfaces();
            if(interfaces.Length > 0)
                foreach(Type t in interfaces)
                    tb.AddInterfaceImplementation(t);
            

            this.type = type;
        }

        public void BuildConstructorWithOneParameter()
        {
            Type[] parameterTypes = new Type[] { typeof(MockMethod[]) };//Type.EmptyTypes;

            cb = tb.DefineConstructor(
                MethodAttributes.Public, 
                CallingConventions.Standard, 
                parameterTypes);

            ILGenerator il = cb.GetILGenerator();
            //ConstructorInfo ci = typeof(object).GetConstructor(Type.EmptyTypes);

            //il.Emit(OpCodes.Ldarg_0);
            //il.Emit(OpCodes.Call, ci);
            //il.Emit(OpCodes.Ret);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);

            ConstructorInfo ci = typeof(helpers.MocksBase).GetConstructor(parameterTypes);
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

            CreateMethodsFromInterfaceHierarchy(type);
        }

        private void CreateMethodsFromInterfaceHierarchy(Type type)
        {
            Type[] interfaces = type.GetInterfaces();

            if (interfaces.Length == 0) //condição de paragem
                return;

            foreach(Type t in interfaces)       //percorre todas as interfaces do tipo
            {
                CreateMethodsFromInterfaceHierarchy(t); // recursividade
                MethodInfo[] iMethods = t.GetMethods(); // obtem todos os métodos da interface

                foreach(MethodInfo mi in iMethods)
                {
                    CreateMethod(mi);       
                }
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
            Type[] parameterType = GetTypeArrayOfMethodParameter(mi);

            MethodBuilder mb = tb.DefineMethod(
                    mi.Name,
                    mAttributes,
                    retType,
                    parameterType);

            ILGenerator il = mb.GetILGenerator();
            //ConstructorInfo ci = typeof(System.NotImplementedException).GetConstructor(Type.EmptyTypes);

            //il.Emit(OpCodes.Newobj, ci);
            //il.Emit(OpCodes.Throw);

            // return InvokeMethod("Add", a, b);
            //IL_0001: ldarg.0
            //IL_0002: ldstr "Add"
            //// (no C# code)
            //IL_0007: ldc.i4.2
            //IL_0008: newarr[mscorlib]System.Object
            //IL_000d: dup
            //IL_000e: ldc.i4.0
            //IL_000f: ldarg.1
            //IL_0010: box[mscorlib]System.Int32
            //IL_0015: stelem.ref
            //IL_0016: dup
            //IL_0017: ldc.i4.1
            //IL_0018: ldarg.2
            //IL_0019: box[mscorlib]System.Int32
            //IL_001e: stelem.ref
            //IL_001f: call instance int32 Mocky.helpers.MocksBase::InvokeMethod(string, object[])
            //IL_0024: stloc.0
            //IL_0025: br.s IL_0027
            //IL_0027: ldloc.0
            //IL_0028: ret


            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldstr, mi.Name);

            int numberOfParamatersInMethod = mi.GetParameters().Length;

            il.Emit(OpCodes.Ldc_I4, numberOfParamatersInMethod);
            il.Emit(OpCodes.Newarr, typeof(object));

            PushParameterArgumentsToObjectArray(il, mi);

            il.Emit(OpCodes.Call, typeof(helpers.MocksBase).GetMethod("InvokeMethod"));


            callReturnValue = tb.DefineField("res", retType, FieldAttributes.Private);

            il.Emit(OpCodes.Stloc, callReturnValue);
            il.Emit(OpCodes.Ldloc, callReturnValue);
            il.Emit(OpCodes.Ret);
        }

        private void PushParameterArgumentsToObjectArray(ILGenerator il, MethodInfo mi)
        {
            ParameterInfo[] pi = mi.GetParameters();

            int numberOfParameters = mi.GetParameters().Length;
            for(int i = 0; i < numberOfParameters; i++)
            {
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);
                il.Emit(OpCodes.Box, pi[i].ParameterType);
                il.Emit(OpCodes.Stelem_Ref);
            }

        }

        private Type[] GetTypeArrayOfMethodParameter(MethodInfo mi)
        {
            ParameterInfo[] pi = mi.GetParameters();

            if (pi.Length == 0)
                return Type.EmptyTypes;

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
