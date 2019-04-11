using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mocky.Emiters
{
    public class CallCreatorBuilder
    {
        Type createdType;
        Type klass;
        private AssemblyName aName;
        private AssemblyBuilder ab;
        private ModuleBuilder mb;
        private TypeBuilder tb;
        private ConstructorBuilder cb;
        private FieldBuilder results;

        public CallCreatorBuilder(Type type)
        {
            aName = new AssemblyName("MethodCallBuilder");
            ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);
            mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            tb = mb.DefineType("MethodCallBuilder", TypeAttributes.Public);
            klass = type;

            CreateField();
            CreateCtor();
            CreateCallMethod();
            CreateType();
        }

        public Type GetTypeWithCallMethod()
        {
            return createdType;
        }

        private void CreateType()
        {
            createdType = tb.CreateType();
            ab.Save(aName.Name + ".dll");
        }

        private void CreateField()
        {
            results = tb.DefineField("results", typeof(Dictionary<object[], object>), FieldAttributes.Private);
        }

        private void CreateCtor()
        {

            Type[] parameterTypes = { typeof(Dictionary<object[], object>) };
            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
            ILGenerator ctorIl = ctor.GetILGenerator();

            ctorIl.Emit(OpCodes.Ldarg_0);
            ctorIl.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            ctorIl.Emit(OpCodes.Ldarg_0);
            ctorIl.Emit(OpCodes.Ldarg_1);
            ctorIl.Emit(OpCodes.Stfld, results);
            ctorIl.Emit(OpCodes.Ret);

            //IL_0000: ldarg.0
            //IL_0001: call instance void [mscorlib]System.Object::.ctor()
            //IL_0006: nop
            //IL_0007: nop
            //// this.results = results;
            //IL_0008: ldarg.0
            //IL_0009: ldarg.1
            //IL_000a: stfld class [mscorlib] System.Collections.Generic.Dictionary`2<object[], object> Mocky.Emiters.MethodBuilderTest::results
            // // (no C# code)
            // IL_000f: ret
        }

        private void CreateCallMethod()
        {
            //throw new NotImplementedException();

            MethodInfo mi = klass.GetMethod("Call");
            MethodAttributes mAttributes = mi.Attributes;
            Type retType = mi.ReturnType;
            Type[] param = GetTypeArrayOfMethodParameter(mi);
            MethodBuilder mb = tb.DefineMethod(mi.Name, mAttributes, retType, param);
            ILGenerator il = mb.GetILGenerator();      

            //local variables
            LocalBuilder dictionary = il.DeclareLocal(typeof(Dictionary<object[], object>));   // locals init [0]
            LocalBuilder pair = il.DeclareLocal(typeof(KeyValuePair<object[], object>));    // locals init [1]
            LocalBuilder key = il.DeclareLocal(typeof(object[]));                           // locals init [2]
            LocalBuilder value = il.DeclareLocal(typeof(object));                           // locals init [3]
            LocalBuilder flag = il.DeclareLocal(typeof(bool));                              // locals init [4]
            LocalBuilder idx = il.DeclareLocal(typeof(int));                                // locals init [5]
            LocalBuilder type = il.DeclareLocal(typeof(Type));                              // locals init [6]
            LocalBuilder cmpObjA = il.DeclareLocal(typeof(object));                         // locals init [7]
            LocalBuilder cmpObjB = il.DeclareLocal(typeof(object));                         // locals init [8]
            LocalBuilder cmpResult = il.DeclareLocal(typeof(bool));                         // locals init [9]
            LocalBuilder forExitCondition = il.DeclareLocal(typeof(bool));                  // locals init [10]
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


            //foreach (KeyValuePair<object[], object> result in results)
            il.Emit(OpCodes.Ldarg_0);
            //il.Emit(OpCodes.Ldfld, GetType().GetField("results"));
            il.Emit(OpCodes.Ldfld, results);
            il.Emit(OpCodes.Callvirt, typeof(Dictionary<object[], object>).GetMethod("GetEnumerator"));
            il.Emit(OpCodes.Stloc, results);

            try
            {
                il.Emit(OpCodes.Br_S, foreachEnd);

                il.MarkLabel(foreachBegin);
                // foreach (KeyValuePair<object[], object> result in results)
                il.Emit(OpCodes.Ldloca_S, results);
                //IL_0015: call instance valuetype [mscorlib]System.Collections.Generic.KeyValuePair`2<!0, !1> valuetype 
                //[mscorlib]System.Collections.Generic.Dictionary`2/Enumerator<object[], object>::get_Current()
                il.Emit(OpCodes.Call, typeof(Dictionary<object[], object>.Enumerator).GetMethod("get_Current"));   //é este o método ou enumerator???
                il.Emit(OpCodes.Stloc, pair);

                // object[] key = result.Key;
                il.Emit(OpCodes.Ldloca_S, pair);
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
                il.Emit(OpCodes.Ldloc_S, idx);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloc_S, type);
                il.Emit(OpCodes.Call, typeof(Convert)
                    .GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type)}));
                il.Emit(OpCodes.Stloc, cmpObjA);

                // object obj2 = Convert.ChangeType(args[i], type);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldloc_S, idx);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloc_S, type);
                il.Emit(OpCodes.Call, typeof(Convert)
                    .GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) }));
                il.Emit(OpCodes.Stloc, cmpObjB);


                // if (!obj.Equals(obj2))
                il.Emit(OpCodes.Ldloc_S, cmpObjA);
                il.Emit(OpCodes.Ldloc_S, cmpObjB);
                il.Emit(OpCodes.Callvirt, typeof(Object)
                    .GetMethod("Equals", new Type[] { typeof(object), typeof(object) }));
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc_S, cmpResult);
                il.Emit(OpCodes.Ldloc_S, cmpResult);
                il.Emit(OpCodes.Brfalse_S, ifNotEquals);


                // flag = false;
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Stloc, flag);
                il.Emit(OpCodes.Br_S, ifFind);

                // for (int i = 0; i < key.Length; i++)
                il.MarkLabel(ifNotEquals);
                il.Emit(OpCodes.Ldloc_S, idx);
                il.Emit(OpCodes.Ldc_I4_1);
                il.Emit(OpCodes.Add);
                il.Emit(OpCodes.Stloc_S, idx);

                // for (int i = 0; i < key.Length; i++)
                il.MarkLabel(forStopCondition); //IL_0079
                il.Emit(OpCodes.Ldloc_S, idx);
                il.Emit(OpCodes.Ldloc_S, key);
                il.Emit(OpCodes.Ldlen);
                il.Emit(OpCodes.Conv_I4);
                il.Emit(OpCodes.Clt);
                il.Emit(OpCodes.Stloc, forExitCondition);
                il.Emit(OpCodes.Ldloc_S, forExitCondition);
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
                il.Emit(OpCodes.Call, typeof(Dictionary<object[], object>.Enumerator).GetMethod("MoveNext"));
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
            il.Emit(OpCodes.Ldloc_S, returnValue);
            il.Emit(OpCodes.Ret);



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
