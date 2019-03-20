using Csvier.Abstract;
using Csvier.Exceptions;
using Csvier.Parsers;
using Csvier.Reflect;
using Csvier.Wrappers;
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
        Dictionary<Type, IReflection> reflectors;
        Dictionary<string, IParserWrapper> parsers;
        PropertyInfo[] pi;
        FieldInfo[] fi;
        ConstructorInfo[] ci;
        readonly char separator;

        public ParserHandler(Type type, char separator)
        {
            this.type = type;
            ctorManager = new CtorManager(type);
            reflectors = new Dictionary<Type, IReflection>();
            parsers = new Dictionary<string, IParserWrapper>();
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

            CsvBasicInfo bInfo = new CsvBasicInfo(name, col, separator);
            PropertyInfo pInfo = type.GetProperty(name);        
            Type propType = pInfo.GetType();                                   // prop Type
            PropertyWrapper pw;
            IReflection reflect;

            if (reflectors.TryGetValue(propType,  out reflect))
            {
                pw = new PropertyWrapper((PropertyReflect)reflect, bInfo);
                parsers.Add(name, pw);
            }
            else
            {
                PropertyReflect pr = new PropertyReflect(propType);
                reflectors.Add(propType, pr);
                pw = new PropertyWrapper(pr, bInfo);
                parsers.Add(name, pw);
            }
            

            //parsers.Add(name, new PropertyReflect(type));
        }

        public void AddField(string name, int col)
        {
            if (parsers.ContainsKey(name))
                throw new FieldException("allready constains a field with that name");
            if (!Array.Exists(fi, f => f.Name == name))
                throw new FieldException($"No such field found in {type.Name}");

            CsvBasicInfo bInfo = new CsvBasicInfo(name, col, separator);
            FieldInfo fInfo = type.GetField(name);
            Type fieldType = fInfo.GetType();                                   // prop Type
            FieldWrapper fw;
            IReflection reflect;

            if (reflectors.TryGetValue(fieldType, out reflect))
            {
                fw = new FieldWrapper((FieldReflect)reflect, bInfo);
                parsers.Add(name, fw);
            }
            else
            {
                FieldReflect fr = new FieldReflect(fieldType);
                reflectors.Add(fieldType, fr);
                fw = new FieldWrapper(fr, bInfo);
                parsers.Add(name, fw);
            }
        }

        public object[] GetObjects(string[] data)
        {
            return ctorManager.CreateObjectArrayData(data); 
        }

        /**
         * */
        public void PopulateFieldAndProperties(object[] src)
        {
            //throw new NotImplementedException();
        }

    }
}
