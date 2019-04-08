using System;
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

        public SimpleEmiter(Type type, string assemblyName) {

            aName = new AssemblyName(assemblyName);
            ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            tb = mb.DefineType("Mock" + type.Name, TypeAttributes.Public);
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

            Type[] interfaces = type.GetInterfaces();

            if(interfaces.Length == 1)
            {
                MethodInfo[] imethods = interfaces[0].GetMethods();
                CreateMethod(imethods[0]);
            }
            

            //foreach (Type anInterface in interfaces)
            //{
            //    var mapp = GetType().GetInterfaceMap(anInterface);
            //    foreach (var info in mapp.TargetMethods)
            //        ;
            //}


        }

        //public void BuildMethodsFlattenHierarchy()
        //{
        //    MethodInfo[] mi = type.GetMethods(BindingFlags.FlattenHierarchy);

        //    if (mi.Length == 0)
        //        return;

        //    foreach (MethodInfo m in mi)
        //    {
        //        CreateMethod(m);
        //    }
        //}

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
            ConstructorInfo ci = typeof(System.NotImplementedException).GetConstructor(Type.EmptyTypes);

            il.Emit(OpCodes.Newobj, ci);
            il.Emit(OpCodes.Throw);
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

        //public void CreateToString()
        //{
        //    //MethodInfo mi = type.GetMethod("ToString", BindingFlags.FlattenHierarchy);

        //    MethodAttributes mAttributes =
        //        MethodAttributes.Public |
        //        MethodAttributes.ReuseSlot |
        //        MethodAttributes.HideBySig |
        //        MethodAttributes.Virtual;

        //    MethodBuilder mb = tb.DefineMethod(
        //            "ToString",
        //            mAttributes,
        //            typeof(string),
        //            Type.EmptyTypes);

        //    ILGenerator il = mb.GetILGenerator();
            
        //    //il.Emit(OpCodes.Ldarg_0);
        //    il.Emit(OpCodes.Ldstr, "Mock");
        //    il.Emit(OpCodes.Ldstr, type.Name);
        //    Type[] objectArgs = { typeof(object), typeof(object) };
        //    il.Emit(OpCodes.Call, typeof(string).GetMethod("Concat", objectArgs));
        //    il.Emit(OpCodes.Ret);
        //}


    }
}
