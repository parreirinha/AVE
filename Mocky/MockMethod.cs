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
            //        if ((int)arg[i] != (int)args[i])
            //        {
            //            find = false;
            //            break;
            //        }
            //    }

            //    if (find)
            //        return val;
            //}
            //return 0;

            // #################################

            IlGeneratorProvider ilProvider = new IlGeneratorProvider(GetType());

            MethodInfo mi = GetType().GetMethod("Call");
            ILGenerator il = ilProvider.GetMethodEmiter(mi);

            LocalBuilder results = il.DeclareLocal(typeof(Dictionary<object[], object>));   // locals init [0]
            LocalBuilder pair = il.DeclareLocal(typeof(KeyValuePair<object[], object>));    // locals init [1]
            LocalBuilder key = il.DeclareLocal(typeof(object[]));                           // locals init [2]
            LocalBuilder value = il.DeclareLocal(typeof(object));                           // locals init [3]
            LocalBuilder flag = il.DeclareLocal(typeof(bool));                              // locals init [4]
            LocalBuilder idx = il.DeclareLocal(typeof(int));                                // locals init [5]
            LocalBuilder cmpResult = il.DeclareLocal(typeof(bool));                         // locals init [6]
            LocalBuilder forExitCondition = il.DeclareLocal(typeof(int));                   // locals init [7]
            LocalBuilder flagVerification = il.DeclareLocal(typeof(bool));                  // locals init [8]
            LocalBuilder returnValue = il.DeclareLocal(typeof(object));                     // locals init [9]

            Label foreachEnd = il.DefineLabel();    // IL_0077
            Label loopStart = il.DefineLabel();     // IL_005c
            Label jumpIf = il.DefineLabel();        // IL_0055
            Label ifAvaluation = il.DefineLabel();  // IL_0031
            Label argNotFound = il.DefineLabel();   // IL_0076
            Label goToReturn = il.DefineLabel();    // IL_009b
            Label foreachBegin = il.DefineLabel();  // IL_0010
            Label returnZero = il.DefineLabel();    // IL_0091

            // foreach (KeyValuePair<object[], object> result in results)
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, this.GetType().GetField("results"));
            il.Emit(OpCodes.Callvirt, typeof(Dictionary < object[], object >).GetMethod("GetEnumerator"));
            il.Emit(OpCodes.Stloc, results);

            try
            {
                il.Emit(OpCodes.Br_S, foreachEnd);

                // foreach (KeyValuePair<object[], object> result in results)
                il.Emit(OpCodes.Ldloca_S, results);
                il.Emit(OpCodes.Call, typeof(Dictionary<object[], object>).GetMethod("get_Current"));
                il.Emit(OpCodes.Stloc, pair);

                // object[] key = result.Key;
                il.Emit(OpCodes.Ldloca, pair);
                il.Emit(OpCodes.Call, typeof(KeyValuePair<object[], object>).GetMethod("get_Key");
                il.Emit(OpCodes.Stloc, key);

                // object value = result.Value;

                // bool flag = true;

                // for (int i = 0; i < key.Length; i++)

                // loop start (head: IL_005c)

                // if ((int)key[i] != (int)args[i])

                // flag = false;


                // for (int i = 0; i < key.Length; i++)


                // for (int i = 0; i < key.Length; i++)

                // end loop

                // if (flag)

                // return value;

                // foreach (KeyValuePair<object[], object> result in results)
                il.MarkLabel(foreachEnd); //IL_0077


                // end loop

                //
            }
            finally
            {

            }

            // return 0;



        }


        private static bool areAllArgumentsCompatible(ParameterInfo[] argTypes, object[] args)
        {
            int i = 0;
            foreach (var p in argTypes) {
                Type a = args[i++].GetType();
                if (!p.ParameterType.IsAssignableFrom(a))
                    return false;
            }
            return true;
        }
        
    }
}