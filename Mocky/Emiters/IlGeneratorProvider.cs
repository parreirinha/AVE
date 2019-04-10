using System;
using System.Collections.Generic;
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
            MethodBuilder mb = tb.DefineMethod("Il" + mi.Name, mAttributes, retType, param);

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

        public void BuildCallMethod(ILGenerator il)
        {
            //local variables
            LocalBuilder results = il.DeclareLocal(typeof(Dictionary<object[], object>));   // locals init [0]
            LocalBuilder pair = il.DeclareLocal(typeof(KeyValuePair<object[], object>));    // locals init [1]
            LocalBuilder key = il.DeclareLocal(typeof(object[]));                           // locals init [2]
            LocalBuilder value = il.DeclareLocal(typeof(object));                           // locals init [3]
            LocalBuilder flag = il.DeclareLocal(typeof(bool));                              // locals init [4]
            LocalBuilder idx = il.DeclareLocal(typeof(int));                                // locals init [5]
            LocalBuilder type = il.DeclareLocal(typeof(Type));                              // locals init [6]
            LocalBuilder cmpObjA = il.DeclareLocal(typeof(object));                         // locals init [7]
            LocalBuilder cmpObjB = il.DeclareLocal(typeof(object));                         // locals init [8]
            LocalBuilder cmpResult = il.DeclareLocal(typeof(bool));                         // locals init [9]
            LocalBuilder forExitCondition = il.DeclareLocal(typeof(bool));                   // locals init [10]
            LocalBuilder flagVerification = il.DeclareLocal(typeof(bool));                  // locals init [11]
            LocalBuilder returnValue = il.DeclareLocal(typeof(object));                     // locals init [12]

            //lables
            Label foreachEnd = il.DefineLabel();            // IL_0094
            Label forStopCondition = il.DefineLabel();      // IL_0079
            Label ifNotEquals = il.DefineLabel();           // IL_0072
            Label ifFind = il.DefineLabel();                // IL_0086
            Label forBegin = il.DefineLabel();              // IL_0034
            Label foreachVerification = il.DefineLabel();   // IL_0093
            Label foreachBegin = il.DefineLabel();          // IL_0013
            Label leaveForeach = il.DefineLabel();          // IL_00b1
            Label retLabel = il.DefineLabel();              // IL_00bb

            // foreach (KeyValuePair<object[], object> result in results)
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, this.GetType().GetField("results"));
            il.Emit(OpCodes.Callvirt, typeof(Dictionary<object[], object>).GetMethod("GetEnumerator"));
            il.Emit(OpCodes.Stloc, results);

            try
            {
                il.Emit(OpCodes.Br_S, foreachEnd);

                il.MarkLabel(foreachBegin);
                // foreach (KeyValuePair<object[], object> result in results)
                il.Emit(OpCodes.Ldloca_S, results);
                il.Emit(OpCodes.Call, typeof(Dictionary<object[], object>).GetMethod("get_Current"));
                il.Emit(OpCodes.Stloc, pair);

                // object[] key = result.Key;
                il.Emit(OpCodes.Ldloca, pair);
                il.Emit(OpCodes.Call, typeof(KeyValuePair<object[], object>).GetMethod("get_Key"));
                il.Emit(OpCodes.Stloc, key);

                // object value = result.Value;
                il.Emit(OpCodes.Ldloca_S, pair);
                il.Emit(OpCodes.Call, typeof(KeyValuePair<object[], object>).GetMethod("get_Key"));
                il.Emit(OpCodes.Stloc, value);

                // bool flag = true;
                il.Emit(OpCodes.Ldc_I4, 1);
                il.Emit(OpCodes.Stloc_S, flag);

                // for (int i = 0; i < key.Length; i++)
                il.Emit(OpCodes.Ldc_I4, 0);
                il.Emit(OpCodes.Stloc_S, idx);

                il.Emit(OpCodes.Br_S, forStopCondition);   //IL_0079
                                                           // loop start (head: IL_0079)

                il.MarkLabel(forBegin);                     // IL_0034
                // Type type = args[i].GetType();
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldloca, idx);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Callvirt, typeof(object).GetType());
                il.Emit(OpCodes.Stloc, type);

                // object obj = Convert.ChangeType(key[i], type);
                il.Emit(OpCodes.Ldloca, key);
                il.Emit(OpCodes.Ldloca_S, idx);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloca_S, type);
                il.Emit(OpCodes.Call, typeof(Convert).GetMethod("ChangeType"));
                il.Emit(OpCodes.Stloc, cmpObjA);

                // object obj2 = Convert.ChangeType(args[i], type);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldloca_S, idx);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloca_S, type);
                il.Emit(OpCodes.Call, typeof(Convert).GetMethod("ChangeType"));
                il.Emit(OpCodes.Stloc, cmpObjB);


                // if (!obj.Equals(obj2))
                il.Emit(OpCodes.Ldloca_S, cmpObjA);
                il.Emit(OpCodes.Ldloca_S, cmpObjB);
                il.Emit(OpCodes.Callvirt, typeof(Object).GetMethod("Equals"));
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc_S, cmpResult);
                il.Emit(OpCodes.Ldloca_S, cmpResult);
                il.Emit(OpCodes.Brfalse_S, ifNotEquals);


                // flag = false;
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Stloc, flag);
                il.Emit(OpCodes.Br_S, ifFind);

                // for (int i = 0; i < key.Length; i++)
                il.MarkLabel(ifNotEquals);
                il.Emit(OpCodes.Ldloca_S, idx);
                il.Emit(OpCodes.Ldc_I4_1);
                il.Emit(OpCodes.Add);
                il.Emit(OpCodes.Stloc_S, idx);

                // for (int i = 0; i < key.Length; i++)
                il.MarkLabel(forStopCondition); //IL_0079
                il.Emit(OpCodes.Ldloca_S, idx);
                il.Emit(OpCodes.Ldloca_S, key);
                il.Emit(OpCodes.Ldlen);
                il.Emit(OpCodes.Conv_I4);
                il.Emit(OpCodes.Clt);
                il.Emit(OpCodes.Stloc, forExitCondition);
                il.Emit(OpCodes.Ldloca_S, forExitCondition);
                il.Emit(OpCodes.Brtrue_S, forBegin);
                // end loop

                // if (flag)
                il.MarkLabel(ifFind);
                il.Emit(OpCodes.Ldloc_S, flag);
                il.Emit(OpCodes.Stloc_S, flagVerification);
                il.Emit(OpCodes.Ldloc_S, flagVerification);
                il.Emit(OpCodes.Brfalse_S, foreachVerification);    //IL_0093

                // return value;
                il.Emit(OpCodes.Ldloca, value);
                il.Emit(OpCodes.Stloc_S, returnValue);
                il.Emit(OpCodes.Leave_S, retLabel); //IL_00bb
                il.MarkLabel(foreachVerification);

                // foreach (KeyValuePair<object[], object> result in results)             
                il.MarkLabel(foreachEnd);
                il.Emit(OpCodes.Ldloca_S, results);
                il.Emit(OpCodes.Call, typeof(Dictionary<object[], object>).GetMethod("MoveNext"));
                il.Emit(OpCodes.Brtrue, foreachBegin);

                // end loop
                il.Emit(OpCodes.Leave_S, leaveForeach);
            }
            finally
            {
                il.Emit(OpCodes.Ldloca_S, results);
                il.Emit(OpCodes.Constrained, typeof(Dictionary<object[], object>)); //IL_00a4: constrained.valuetype[mscorlib] System.Collections.Generic.Dictionary`2 / Enumerator<object[], object>
                il.Emit(OpCodes.Callvirt, typeof(IDisposable).GetMethod("Dispose"));
                il.Emit(OpCodes.Endfinally);
            }

            il.MarkLabel(leaveForeach);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Box, typeof(Int32));
            il.Emit(OpCodes.Stloc_S, returnValue);
            il.MarkLabel(retLabel);
            il.Emit(OpCodes.Ldloca_S, returnValue);
            il.Emit(OpCodes.Ret);
        }
    }
}
