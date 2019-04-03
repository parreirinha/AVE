using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Mocky
{
    public class Mocker
    {
        private readonly Type klass;
        private Dictionary<string, MockMethod> ms;

        public Mocker(Type klass)
        {
            this.klass = klass;
            this.ms = new Dictionary<string, MockMethod>();
        }

        public MockMethod When(string name)
        {
            MockMethod m;
            if (!ms.TryGetValue(name, out m))
            {
                m = new MockMethod(klass, name);
                ms.Add(name, m);
            }
            return m;
        }

        public object Create()
        {
            Type t = BuildType();
            return Activator.CreateInstance(t, new object[1] { ms.Values.ToArray() });
        }

        private Type BuildType()
        {
            // !!!!!! TO DO !!!!!!
            //throw new NotImplementedException();

            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

            TypeBuilder tb = mb.DefineType("MyType", TypeAttributes.Public);
            tb.AddInterfaceImplementation(klass);

            //need to confirm if Fields are needed by FieldBuilder

            // ctor sem parametros
            ConstructorBuilder ctorBuilder = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                Type.EmptyTypes);

            MethodInfo[] mi = klass.GetMethods();

            // add methods to the Type
            foreach(MethodInfo mInfo in mi)
            {
                ParameterInfo[] pi = mInfo.GetParameters();
                Type[] paramtype = GetParamTypes(pi);
                MethodBuilder methodBuilder = tb.DefineMethod(
                    mInfo.Name, 
                    mInfo.Attributes,                   //ver estes atributos
                    mInfo.ReturnParameter.GetType(),
                    paramtype);

                ILGenerator methodIl = methodBuilder.GetILGenerator();
                //newobj instance void [mscorlib]System.NotImplementedException::.ctor()
                methodIl.Emit(OpCodes.Newobj, typeof(NotImplementedException)); // aqui enviar excepção
            }

            ILGenerator il = ctorBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            //call instance void [mscorlib]System.Object::.ctor()
            il.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));    // o que passar aqui???
            il.Emit(OpCodes.Ret);

            return tb.CreateType();
        }

        private Type[] GetParamTypes(ParameterInfo[] pi)
        {
            Type[] res = new Type[pi.Length];

            int idx = 0;
            foreach(ParameterInfo p in pi)
            {
                res[idx++] = p.ParameterType;
            }

            return res;
        }
    }
}
