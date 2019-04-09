using System;
using System.Reflection;
using System.Reflection.Emit;


namespace Mocky.Emiters
{
    public class IlGeneratorProvider
    {
        private AssemblyName aName;
        private AssemblyBuilder ab;
        private ModuleBuilder mb;
        private TypeBuilder tb;
        private ConstructorBuilder cb;
        Type type;

        public IlGeneratorProvider(Type type)
        {
            this.type = type;
            aName = new AssemblyName("IlGeneratorProvider");
            ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            tb = mb.DefineType(type.Name + "IlProvider", TypeAttributes.Public);
        }

        public ILGenerator GetMethodEmiter(MethodInfo mi)
        {
            MethodAttributes mAttributes = mi.Attributes;
            Type retType = mi.ReturnType;
            Type[] param = GetTypeArrayOfMethodParameter(mi);
            MethodBuilder mb = tb.DefineMethod(mi.Name, mAttributes, retType, param);

            return mb.GetILGenerator();
        }


        private Type[] GetTypeArrayOfMethodParameter(MethodInfo mi)
        {
            ParameterInfo[] pi = mi.GetParameters();

            if (pi.Length == 0)
                return Type.EmptyTypes;

            Type[] res = new Type[pi.Length];

            for (int i = 0; i < pi.Length; i++)
            {
                res[i] = pi[i].ParameterType;
            }

            return res;
        }
    }
}
