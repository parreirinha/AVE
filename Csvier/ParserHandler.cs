using Csvier.Abstract;
using Csvier.Exceptions;
using Csvier.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Csvier
{
    class ParserHandler
    {
        private Type type;
        CtorManager ctorManager;
        Dictionary<String, IParser> parsers;
        PropertyInfo[] pi;
        FieldInfo[] fi;
        ConstructorInfo[] ci;
        readonly char separator;

        public ParserHandler(Type type, char separator)
        {
            this.type = type;
            ctorManager = new CtorManager(type);
            parsers = new Dictionary<string, IParser>();
            pi = type.GetProperties();
            fi = type.GetFields();
            ci = type.GetConstructors();
            this.separator = separator;
        }

        public void AddCtorParam(string name, int col)
        {
            if (!CtorHasParam(name))
                throw new CtorException($"No override of Ctor of class {type.Name} has a parameter with the name {name}");
            if(ctorManager.constainsParameter(name))
                throw new CtorException($"The parameter {name} was allready added to the ctor");

            ctorManager.AddCtorParameter(name, col);
        }

        // validate if exists the named parameter in all ctor overrides
        private bool CtorHasParam(string paramName)
        {
            bool exists = false;
            foreach (ConstructorInfo cInfo in ci)
            {
                ParameterInfo[] pi = cInfo.GetParameters();
                exists = Array.Exists<ParameterInfo>(pi, elem => elem.Name == paramName);
                if (exists)
                    return true;
            }
            return exists;
        }

        public void AddProp(string name, int col)
        {
            if (parsers.ContainsKey(name))
                throw new PropertyException("Allready constains a property with that name");
            if(!Array.Exists(pi, p => p.Name == name))
                throw new PropertyException($"No such property found in {type.Name}");

            parsers.Add(name, new PropertyParser(type, name, col, separator));
        }

        public void AddField(string name, int col)
        {
            if (parsers.ContainsKey(name))
                throw new FieldException("allready constains a field with that name");
            if (!Array.Exists(fi, f => f.Name == name))
                throw new FieldException($"No such field found in {type.Name}");

            parsers.Add(name, new FieldParser(type, name, col, separator));
        }

        public object[] GetObjects(string[] data)
        {
            return ctorManager.CreateObjectArrayData(data); 
        }

    }
}
