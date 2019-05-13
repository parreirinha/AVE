using Csvier.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Csvier
{
    class CtorManager<T>
    {
        private Type type;
        private Dictionary<String, int> ctorParams;
        private ConstructorInfo[] ci;
        private char separator;

        public CtorManager(char separator)
        {
            this.type = typeof(T);
            this.separator = separator;
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

        // returns the ctor that will be used to create the instances
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
         * retorns the created objects
         * */
        public IEnumerable<T> CreateObjectArrayData(string[] data)
        {
            if (data == null || data.Length == 0)
                throw new CtorException("there are no data to populate the object");

            List<T> res = new List<T>();

            for(int i=0; i<data.Length; i++)
            {
                //res[i] = BuildInstance(data[i]);
                res.Add(BuildInstance(data[i]));
            }
            return res;
        }

        /**
         *  Build a new object from a single line
         *  data is a single line of the csv file
         ***/
        private T BuildInstance(string data)
        {
            string[] values = data.Split(separator);

            object[] parameters = GetParametersForCtor(values); 
            T retValue = (T)Activator.CreateInstance(type, parameters);
            return retValue;
        }


        /**
         * retorna um array de objectos com os valores finais para passar ao Activator.CreateInstance
         * */
        private object[] GetParametersForCtor(string[] data)
        {
            ConstructorInfo currCtor = FindCtor();
            ParameterInfo[] pi = currCtor.GetParameters();
            ParameterInfo currParam;
            object[] parameters = new object[ctorParams.Count];

            int idx = 0;
            foreach(KeyValuePair<string, int> pair in ctorParams)
            {
                currParam = pi[idx];
                Type paramType = currParam.ParameterType;
                parameters[idx++] = GetParameterParsed(pair.Key, data[pair.Value], paramType);
            }
            return parameters;
        }

        /*
         * Parse line values(strings) to "real type" value. 
         * Makes the correspondence between csv file and the object WeatherInfo, locationInfo, etc...
         * */
        private object GetParameterParsed(string paramName, string data, Type paramType)
        {
            if (paramType == typeof(string))
                return data;

            MethodInfo mi = paramType.GetMethod("Parse", new Type[] { typeof(string) });
            object val = mi.Invoke(paramType, new object[] {data });

            return val;
        }
    }
}
