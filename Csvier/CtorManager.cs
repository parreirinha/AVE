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

        public CtorManager(Type type)
        {
            this.type = type;
            ctorParams = new Dictionary<string, int>();
            ci = type.GetConstructors();
        }

        public object BuildInstance(string[] data)
        {
            object[] parameters = GetParametersForCtor();
            object retValue = Activator.CreateInstance(type, parameters);
            return retValue;
        }

        private object[] GetParametersForCtor()
        {
            ConstructorInfo currCtor = FindCtor();


            throw new NotImplementedException();
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
                    if (pair.Key.Equals(currCtorInfo.Name))
                        continue;
                    find = false;
                }

                if (find)
                    return currCtorInfo;
            }
            throw new CtorException($"arguments inserted in CtorArgs aren't compatible with object of type {type.Name}");
        }

        public void AddCtorParameter(string paramName, int col)
        {
            if (!CtorHasParam(paramName))
                throw new CtorException($"Invalid parameter exception for ctor of class {type.Name}");
            ctorParams.Add(paramName, col);
        }

        private bool CtorHasParam(string paramName)
        {
            //Array.Exists
            bool exists = false;
            foreach(ConstructorInfo cInfo in ci)
            {
                ParameterInfo[] pi = cInfo.GetParameters();
                exists = Array.Exists<ParameterInfo>(pi, elem => elem.Name == paramName);
                if (exists)
                    return true;
            }
            return exists;
        }


    }
}
