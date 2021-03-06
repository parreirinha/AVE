﻿using Csvier.Abstract;
using Csvier.Cache;
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
    class ParserHandler<T>
    {
        private Type type;
        CtorManager<T> ctorManager;
        Dictionary<string, IParserWrapper> parsers; // IParserWrapper => CsvBasicInfo + IReflect object
        PropertyInfo[] pi;
        FieldInfo[] fi;
        ConstructorInfo[] ci;
        readonly char separator;

        public ParserHandler(char separator)
        {
            this.type = typeof(T);
            ctorManager = new CtorManager<T>(separator);
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

            CsvBasicInfo bInfo = new CsvBasicInfo(type, name, col, separator);
            PropertyInfo pInfo = type.GetProperty(name);
            Type propType = pInfo.PropertyType;                                 // prop Type
            PropertyWrapper pw;
            IReflection reflect;

            if (ReflectorsCache.cache.TryGetValue(propType, out reflect))   
            {
                // if reflect of type propType exists, get it from cache
                pw = new PropertyWrapper((PropertyReflect)reflect, bInfo);
                parsers.Add(name, pw);
            }
            else
            {
                //if not build a new onde, add to cache and create PropertyWrapper 
                //and add it to parsers Dictionary
                PropertyReflect pr = new PropertyReflect(propType);
                ReflectorsCache.cache.Add(propType, pr);
                pw = new PropertyWrapper(pr, bInfo);
                parsers.Add(name, pw);
            }
        }

        public void AddField(string name, int col)
        {
            if (parsers.ContainsKey(name))
                throw new FieldException("allready constains a field with that name");
            if (!Array.Exists(fi, f => f.Name == name))
                throw new FieldException($"No such field found in {type.Name}");

            CsvBasicInfo bInfo = new CsvBasicInfo(type, name, col, separator);
            FieldInfo fInfo = type.GetField(name);
            Type fieldType = fInfo.FieldType;                                   // prop Type
            FieldWrapper fw;
            IReflection reflect;

            if (ReflectorsCache.cache.TryGetValue(fieldType, out reflect))
            {
                fw = new FieldWrapper((FieldReflect)reflect, bInfo);
                parsers.Add(name, fw);
            }
            else
            {
                FieldReflect fr = new FieldReflect(fieldType);
                ReflectorsCache.cache.Add(fieldType, fr);
                fw = new FieldWrapper(fr, bInfo);
                parsers.Add(name, fw);
            }
        }

        public IEnumerable<T> GetObjects(string[] data)
        {
            return ctorManager.CreateObjectArrayData(data); 
        }

        /**
         * this method will set all properties and fields values in src object with the values of data
         * */
        public void SetFieldAndPropertiesValues(object src, string data)
        {
            foreach(KeyValuePair<string, IParserWrapper> pw in parsers)
            {
                IParserWrapper parser = pw.Value;
                string name = pw.Key;
                CsvBasicInfo bInfo = parser.GetBasicInfo();
                string[] arrayData = data.Split(bInfo.Separator);

                parser.SetValue(arrayData, src);
            }
        }

    }
}
