using Mocky.Emiters;
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

            // AssemblyName aName = new AssemblyName("DynamicAssembly");
            // AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(
            //         aName,
            //         AssemblyBuilderAccess.RunAndSave);

            // ModuleBuilder mb = ab.DefineDynamicModule(
            //         aName.Name,
            //         aName.Name + ".dll");

            // TypeBuilder tb = mb.DefineType(
            //         "MyType",
            //         TypeAttributes.Public);

            // tb.AddInterfaceImplementation(klass);

            // ctor sem parametros
            //ConstructorBuilder ctorBuilder = tb.DefineConstructor(
            //        MethodAttributes.Public,
            //        CallingConventions.Standard,
            //        Type.EmptyTypes);

            // ILGenerator il = ctorBuilder.GetILGenerator();
            // il.Emit(OpCodes.Ldarg_0);
            // il.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));    // o que passar aqui???
            // il.Emit(OpCodes.Ret);


            // MethodInfo[] mi = klass.GetMethods();

            // add methods to the Type
            // foreach (MethodInfo mInfo in mi)
            // {
            //     ParameterInfo[] pi = mInfo.GetParameters();
            //     Type[] paramtype = GetParamTypes(pi);

            //     MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final;
            //     MethodBuilder methodBuilder = tb.DefineMethod(
            //         mInfo.Name,
            //         attributes,                   //ver estes atributos
            //         mInfo.ReturnParameter.GetType(),
            //         paramtype);

            //     ILGenerator methodIl = methodBuilder.GetILGenerator();
            //     newobj instance void [mscorlib]System.NotImplementedException::.ctor()
            //     ConstructorInfo ci = typeof(NotImplementedException).GetConstructor(Type.EmptyTypes);
            //     methodIl.Emit(OpCodes.Newobj, ci);
            //     methodIl.Emit(OpCodes.Throw);
            // }


            // Type t = tb.CreateType();
            // ab.Save(aName.Name + ".dll");
            // return t;


            SimpleEmiter emiter = new SimpleEmiter(klass,"DynamicAssembly", "MyType");
            emiter.BuildConstructorWithoutParameters();
            emiter.BuildMethods();
            Type createdType = emiter.CreateType();

            return createdType;
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
