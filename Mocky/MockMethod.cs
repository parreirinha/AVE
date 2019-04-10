using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Mocky.Emiters;

namespace Mocky
{
    public class MockMethod
    {
        private readonly Type klass;
        private readonly MethodInfo meth;
        private Dictionary<object[], object> results;
        private object[] args;

        public MethodInfo Method { get { return meth;  } }

        public MockMethod(Type type, string name)
        {
            this.klass = type;
            this.meth = type.GetMethod(name);
            if (meth == null)
                throw new ArgumentException("There is no method " + name + " in type " + type);
            this.results = new Dictionary<object[], object>();

        }

        public MockMethod With(params object[] args)
        {
            if (this.args != null)
                throw new InvalidOperationException("You already called With() !!!!  Cannot call it twice without calling Return() first!");
            ParameterInfo[] argTypes = meth.GetParameters();
            if (argTypes.Length == args.Length) {
                if (areAllArgumentsCompatible(argTypes, args))
                {
                    this.args = args;
                    return this;
                }
            }
            throw new InvalidOperationException("Invalid arguments: " + String.Join(",", args));
        }

        public void Return(object res)
        {
            results.Add(args, res);
            this.args = null;
        }

        private static bool areAllArgumentsCompatible(ParameterInfo[] argTypes, object[] args)
        {
            int i = 0;
            foreach (var p in argTypes)
            {
                Type a = args[i++].GetType();
                if (!p.ParameterType.IsAssignableFrom(a))
                    return false;
            }
            return true;
        }

        public object Call(params object [] args)
        {
            // !!!!! TO DO !!!!!

            //throw new NotImplementedException();


            //LABELS
            //Label noLog = il.DefineLabel();
            //il.Emit(OpCodes.Brtrue, noLog);
            //il.MarkLabel(noLog);

            //locals init
            //LocalBuilder a = ilGen.DeclareLocal(typeof(Int32));


            //foreach (KeyValuePair<object[], object> pair in results)
            //{
            //    Object[] arg = pair.Key;
            //    Object val = pair.Value;
            //    bool find = true;

            //    for(int i = 0; i < arg.Length; i++)
            //    {
                    
            //        Type t = (args[i]).GetType();

            //        var a = Convert.ChangeType(arg[i], t);
            //        var b = Convert.ChangeType(args[i], t);

            //        if (!a.Equals(b))
            //        {
            //            find = false;
            //            break;
            //        }
            //    }

            //    if (find)
            //        return val;
            //}
            //return 0;



            // il generator
            IlGeneratorProvider ilProvider = new IlGeneratorProvider(GetType());
            MethodInfo mi = GetType().GetMethod("Call");
            ILGenerator il = ilProvider.GetMethodEmiter(mi);

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
            Label retValue = il.DefineLabel();              // IL_00bb

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

				IL_005a: ldloc.s 7
				IL_005c: ldloc.s 8
				IL_005e: callvirt instance bool[mscorlib] System.Object::Equals(object)
                IL_0063: ldc.i4.0
				IL_0064: ceq
                IL_0066: stloc.s 9
				IL_0068: ldloc.s 9
				IL_006a: brfalse.s IL_0072



                // flag = false;
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);

                IL_006d: ldc.i4.0
				IL_006e: stloc.s 4
				IL_0070: br.s IL_0086



                // for (int i = 0; i < key.Length; i++)
                il.MarkLabel(ifNotEquals);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
    
                IL_0073: ldloc.s 5
				IL_0075: ldc.i4.1
				IL_0076: add
                IL_0077: stloc.s 5

                // for (int i = 0; i < key.Length; i++)
                il.MarkLabel(forStopCondition); //IL_0079
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);

				IL_0079: ldloc.s 5
				IL_007b: ldloc.2
				IL_007c: ldlen
                IL_007d: conv.i4
                IL_007e: clt
                IL_0080: stloc.s 10
				IL_0082: ldloc.s 10
				IL_0084: brtrue.s IL_0034
            // end loop

            // if (flag)
            il.MarkLabel(ifFind);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);

            IL_0086: ldloc.s 4
			IL_0088: stloc.s 11
			IL_008a: ldloc.s 11
			IL_008c: brfalse.s IL_0093

            // return value;
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.MarkLabel(foreachVerification);

            IL_008e: ldloc.3
			IL_008f: stloc.s 12
			IL_0091: leave.s IL_00bb
            

            // foreach (KeyValuePair<object[], object> result in results)
               
            il.MarkLabel(foreachEnd);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);

            IL_0094: ldloca.s 0
			IL_0096: call instance bool valuetype[mscorlib]System.Collections.Generic.Dictionary`2/Enumerator<object[], object>::MoveNext()
            IL_009b: brtrue IL_0013

        // end loop
        il.Emit(OpCodes);
        IL_00a0: leave.s IL_00b1


            }
            finally
            {
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                il.Emit(OpCodes);
                //IL_00a2: ldloca.s 0
                //IL_00a4: constrained.valuetype[mscorlib] System.Collections.Generic.Dictionary`2/Enumerator<object[], object>
                //IL_00aa: callvirt instance void[mscorlib] System.IDisposable::Dispose()
                //IL_00af: nop
                //IL_00b0: endfinally

            }

            il.MarkLabel(leaveForeach);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.Emit(OpCodes);
            il.MarkLabel(retValue);
            il.Emit(OpCodes);
            il.Emit(OpCodes);

            //   // return 0;
            //   IL_00b1: ldc.i4.0
            //   IL_00b2: box[mscorlib] System.Int32
            //   IL_00b7: stloc.s 12
            //   // (no C# code)
            //   IL_00b9: br.s IL_00bb
            //   IL_00bb: ldloc.s 12
            //   IL_00bd: ret
        




        
    }
}