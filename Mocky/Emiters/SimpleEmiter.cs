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
                    CreateMethod(mi);       // implementa-os com throw NotImplementedException
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

    }
}
