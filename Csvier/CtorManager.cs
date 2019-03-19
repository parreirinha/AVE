using Csvier.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier
{
    class CtorManager
    {

        private Type type;
        private Dictionary<String, int> ctorParams;
        private ConstructorInfo[] ci;
        private string[] data;

        public CtorManager(Type type)
        {
            this.type = type;
            ctorParams = new Dictionary<string, int>();
            ci = type.GetConstructors();
        }

        public void AddCtorParameter(string paramName, int col)
        {
            ctorParams.Add(paramName, col);
        }

        public bool constainsParameter(string name)
        {
            return ctorParams.ContainsKey(name);
        }


        private ConstructorInfo FindCtor()
        {  
            foreach (ConstructorInfo currCtorInfo in ci)
            {
                ParameterInfo[] pi = currCtorInfo.GetParameters();

                if (ctorParams.Count == 0 && pi.Length == 0)    // Ctor sem parametros
                    return null;

                if (pi.Length != ctorParams.Count)
                    continue;

                bool find = true;
                foreach (KeyValuePair<string, int> pair in ctorParams)
                {
                    if(Array.Exists(pi, p => p.Name == pair.Key))
                        continue;
                    find = false;
                }

                if (find)
                    return currCtorInfo;
            }
            throw new CtorException($"arguments inserted in CtorArgs aren't compatible with object of type {type.Name}");
        }

        /*
         * retorna
         * */
        public object[] CreateObjectArrayData(string[] data)
        {
            if (data == null || data.Length == 0)
                throw new Exception("there are no data to populate the object");

            object[] res = new object[data.Length];

            for(int i=0; i<data.Length; i++)
            {
                res[i] = BuildInstance(data[i]);
            }
            return res;
        }

        /**
         *  Build a new object from a single line
         *  data is a single line of the csv file
         ***/
        private object BuildInstance(string data)
        {
            string[] values = data.Split(',');

            object[] parameters = GetParametersForCtor(data); //TODO
            object retValue = Activator.CreateInstance(type, parameters);
            return retValue;
        }


        //TODO
        /**
         * retorna um array de objectos com os valores finais para passar ao Activator.CreateInstance
         * */
        private object[] GetParametersForCtor(string data)
        {
            ConstructorInfo currCtor = FindCtor();
            ParameterInfo[] pi = currCtor.GetParameters();
            ParameterInfo currParam;
            object[] parameters = new object[ctorParams.Count];

            int idx = 0;
            string[] values = data.Split(',');
            foreach(KeyValuePair<string, int> pair in ctorParams)
            {
                currParam = pi[idx];
                Type paramType = currParam.ParameterType;
                parameters[idx++] = GetParameterParsed(pair.Key, values[pair.Value], paramType);
            }
            return parameters;
        }

        private object GetParameterParsed(string paramName, string data, Type paramType)
        {
            //ci.GetParameters()
            BindingFlags bi = BindingFlags.Instance | BindingFlags.Public;
            MethodInfo mi = paramType.GetMethod("Parse", new Type[] { typeof(string) });
            object val = mi.Invoke(paramType, new object[] {data });


            //object parameter = Activator.CreateInstance(paramType, new object[] { val });

            return val;
        }
    }
}
