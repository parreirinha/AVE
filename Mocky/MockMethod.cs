﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Mocky.Emiters;
using System.Linq;

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
            if(name.Equals("Parse"))
            {
                MethodInfo[] mi = type.GetMethods()
                    .Where(elem => elem.Name == "Parse")
                    .ToArray();

                if (args == null)
                    meth = mi[0];
                else
                    meth = mi[1];
            }
            else
            {
                this.meth = type.GetMethod(name);
            }
            
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

        public object Call(params object[] args)
        {
            // !!!!! TO DO !!!!!

            //throw new NotImplementedException();

            //CallCreatorBuilder ccb = new CallCreatorBuilder(GetType(), args);
            //Type createdType = ccb.GetTypeWithCallMethod();
            //object instance = Activator.CreateInstance(createdType, results);
            //MethodInfo methodInfo = createdType.GetMethod("Call");
            //Object res = methodInfo.Invoke(instance, args);
            //return res;

            //MethodBuilderTest mbt = new MethodBuilderTest(results);
            //return mbt.Call(args);

            foreach (KeyValuePair<object[], object> pair in results)
            {
                Object[] arg = pair.Key;
                Object val = pair.Value;
                bool find = true;

                for (int i = 0; i < arg.Length; i++)
                {
                    Type t = (arg[i]).GetType();

                    var a = arg[i];
                    var b = Convert.ChangeType(args[i], t);

                    if (!a.Equals(b))
                    {
                        find = false;
                        break;
                    }
                }

                if (find)
                    return val;
            }
            return 0;

        }
    }
}