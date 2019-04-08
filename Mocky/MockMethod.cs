using System;
using System.Collections.Generic;
using System.Reflection;

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

            foreach(KeyValuePair<object[], object> pair in results)
            {
                Object[] arg = pair.Key;
                Object val = pair.Value;
                bool flag = true;

                for(int i = 0; i < arg.Length; i++)
                {
                    if ((int)arg[i] != (int)args[i])
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                    return val;
            }
            return 0;
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